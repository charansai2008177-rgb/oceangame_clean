using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIPackage
{
    struct BoneTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public class Ragdoll : MonoBehaviour
    {
        Rigidbody[] rigs;
        CapsuleCollider capsuleCollider;
        Animator animator;
        NavMeshAgent agent;

        Stats stats;
        Rigidbody pelvis;
        BoneTransform[] standUpBones;
        BoneTransform[] ragdollBones;
        Transform[] bones;

        [SerializeField]
        private float timeToResetBones;
        private float elapsedResetBonesTime;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            stats = GetComponent<Stats>();

            pelvis = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Rigidbody>();
            bones = pelvis.GetComponentsInChildren<Transform>();
            
            standUpBones = new BoneTransform[bones.Length];
            ragdollBones = new BoneTransform[bones.Length];
            for (int i = 0; i < bones.Length; i++)
            {
                standUpBones[i] = new BoneTransform();
                ragdollBones[i] = new BoneTransform();
            }

            Populate_BoneTransform_By_AnimationClip_Start_Frame("StandUp");
        }

        void Start()
        {
            rigs = GetComponentsInChildren<Rigidbody>();
            SwitchChildRigs(true);
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = true;
        }

        void SwitchChildRigs(bool on, bool connectWeapon = false)
        {
            foreach (Rigidbody rigidbody in rigs)
            {
                Rigidbody mainRigidBody = GetComponent<Rigidbody>();
                if (rigidbody != mainRigidBody)
                {
                    rigidbody.isKinematic = on;
                    if (connectWeapon)
                    {
                        Connect_Weapon_To_Hand(rigidbody);
                    }
                }
            }
        }

        void Connect_Weapon_To_Hand(Rigidbody rigidbody)
        {
            if (rigidbody.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                rigidbody.isKinematic = true;
                rigidbody.transform.localPosition = new Vector3(0, 0, 0);
                rigidbody.transform.localEulerAngles = new Vector3(-4.3f, -77.8f, -1.2f);
            }
        }

        NpcBase enabled_NpcBase_Before_Ragdoll;

        public void ActivateRagdoll()
        {
            GetComponent<Rotate>().enabled = false;
            animator.enabled = false;
            agent.enabled = false;
            capsuleCollider.enabled = false;

            SwitchChildRigs(false);
        }

        public void ActivateRagdoll(NpcBase npcBase = null)
        {
            GetComponent<Rotate>().enabled = false;
            animator.enabled = false;
            agent.enabled = false;
            capsuleCollider.enabled = false;

            if (stats.isDead)
            {
                SwitchChildRigs(false);
            }
            else
            {
                enabled_NpcBase_Before_Ragdoll = npcBase;
                SwitchChildRigs(false, true);
                StartCoroutine(GetNpcUp());
            }

        }

        IEnumerator GetNpcUp()
        {
            enabled_NpcBase_Before_Ragdoll.enabled = false;

            yield return new WaitForSeconds(0.02f);
            yield return new WaitUntil(() => pelvis.linearVelocity.magnitude < 0.1f);

            Change_Parent_To_PelvisTransform();
            Populate_BoneTransforms(ragdollBones);

            StartCoroutine(Reset_Bones_To_StandUp_Clip());
        }

        void Change_Parent_To_PelvisTransform()
        {
            pelvis.transform.parent = null;
            transform.position = pelvis.transform.position + parent_Pelvis_Pos_Dif;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            pelvis.transform.parent = transform.GetChild(0).GetChild(0);
        }

        public void DisableRagdoll()
        {
            capsuleCollider.enabled = true;
            SwitchChildRigs(true);
            animator.enabled = true;
            agent.enabled = true;
        }

        void Populate_BoneTransforms(BoneTransform[] boneTransforms)
        {
            for (int i = 0; i < boneTransforms.Length; i++)
            {
                boneTransforms[i].position = bones[i].localPosition;
                boneTransforms[i].rotation = bones[i].localRotation;
            }
        }

        Vector3 parent_Pelvis_Pos_Dif;

        void Populate_BoneTransform_By_AnimationClip_Start_Frame(string clipName)
        {
            Vector3 positionBeforeSampling = transform.position;
            Quaternion rotationBeforeSampling = transform.rotation;

            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    clip.SampleAnimation(gameObject, 0);
                    parent_Pelvis_Pos_Dif = transform.position - bones[0].position;
                    Populate_BoneTransforms(standUpBones);
                    break;
                }
            }

            transform.position = positionBeforeSampling;
            transform.rotation = rotationBeforeSampling;
        }

        IEnumerator Reset_Bones_To_StandUp_Clip()
        {
            elapsedResetBonesTime = 0;
            while (true)
            {
                elapsedResetBonesTime += Time.fixedDeltaTime;
                float elapsedPercentage = elapsedResetBonesTime / timeToResetBones;

                for (int i = 0; i < bones.Length; i++)
                {
                    bones[i].localPosition = Vector3.Lerp(ragdollBones[i].position, standUpBones[i].position, elapsedPercentage);
                    bones[i].localRotation = Quaternion.Lerp(ragdollBones[i].rotation, standUpBones[i].rotation, elapsedPercentage);
                }
                
                if (elapsedPercentage >= 1)
                {
                    StartCoroutine(StandUp_And_Disable_Ragdoll());  
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator StandUp_And_Disable_Ragdoll()
        {
            animator.Play("Default.StandUp", 0);
            DisableRagdoll();

            yield return new WaitForSeconds(3.5f);
            enabled_NpcBase_Before_Ragdoll.enabled = true;
            if (enabled_NpcBase_Before_Ragdoll is NpcAI)
            {
                enabled_NpcBase_Before_Ragdoll.ChangeState(NpcState.Scared);    
            }
        }
    }
}