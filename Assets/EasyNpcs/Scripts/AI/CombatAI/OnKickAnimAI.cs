using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIPackage
{
    public class OnKickAnimAI : StateMachineBehaviour
    {
        public AudioClip kickSound;
        public AudioClip kickSwish;
        AudioSource audioSource;

        bool trigger = true;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (audioSource == null)
                audioSource = animator.GetComponent<AudioSource>();
            PlaySound(kickSwish);

            trigger = true;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > 0.7f && trigger)
            {
                PlaySound(kickSound);

                Transform target = animator.GetComponent<Observe>().currentTarget;
                Animator targetAnim = target.GetComponent<Animator>();
                if (targetAnim.GetBool("Block"))
                {
                    targetAnim.SetTrigger("KickImpact");
                }

                trigger = false;
            }
        }

        void PlaySound(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

}