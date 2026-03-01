using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace AIPackage
{
    [RequireComponent(typeof(Observe))]
    public class CombatAI : NpcBase
    {
        public Transform guardPost;
        public List<Transform> patrolSpots;
        int spotNum = 0;

        protected Rotate rotate;

        protected override void Awake()
        {
            base.Awake();
            observe = GetComponent<Observe>();
            rotate = GetComponent<Rotate>();
            anim = GetComponent<Animator>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            agent.stoppingDistance = 0.5f;
            anim.SetBool("Combat", true);

            if (stats.weapon != null)
                stats.weapon.SetActive(true);

            Ragdolled_By_Enemy();
        }

        void Ragdolled_By_Enemy()
        {
            if (observe.currentTarget != null)
            {
                ChangeState(NpcState.Chase);
            }
        }

        protected override void OnIdle()
        {
            agent.speed = stats.walkSpeed;
            ChangeState(NpcState.Patrol);
        }

        protected override void OnPatrol()
        {
            if (patrolSpots.Count > 0)
            {
                PatrolSpots();
            }
            else if (guardPost != null)
            {
                agent.SetDestination(guardPost.position);
                StartCoroutine(AllignToGuardPost());
            }
        }

        IEnumerator AllignToGuardPost()
        {
            yield return new WaitUntil(() => Vector3.Distance(agent.destination, guardPost.position) < agent.stoppingDistance);
            rotate.RotateTo(guardPost.position + guardPost.forward);
        }

        void PatrolSpots()
        {
            if (spotNum >= patrolSpots.Count)
            {
                spotNum = 0;
            }

            agent.SetDestination(patrolSpots[spotNum].position);
            spotNum++;

            StartCoroutine(GoTo_PatrolSpot());
        }

        public UnityEvent onAttack_Event;

        protected override void OnAttack()
        {
            agent.SetDestination(transform.position);
            rotate.RotateTo(observe.currentTarget);
            StartCoroutine(IfTargetDistance_IsLonger_ThanAttackDistance_Chase());

            onAttack_Event.Invoke();
        }

        IEnumerator IfTargetDistance_IsLonger_ThanAttackDistance_Chase()
        {
            do
            {
                if (observe.currentTarget != null)
                {
                    if (observe.CheckTarget_Distance_AndRaycast())
                    {
                        Trigger_Attack_Anim();
                    }
                    else if (anim.GetInteger("Action") == -1)
                    {
                        ChangeState(NpcState.Chase);
                        break;
                    }
                }
                else
                {
                    ChangeState(NpcState.Default);
                    break;
                }

                yield return new WaitForFixedUpdate();

            } while (true);
        }

        public float patrolTime = 3;

        IEnumerator GoTo_PatrolSpot()
        {
            Vector3 pos = agent.destination;

            yield return new WaitUntil(() => Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance);
            anim.SetTrigger("LookAround");

            yield return new WaitForSeconds(patrolTime);
            ChangeState(NpcState.Default);
        }

        Observe observe;

        protected override void OnChase()
        {
            agent.SetDestination(observe.currentTarget.position);
            StartCoroutine(DuringChasing());
        }

        IEnumerator DuringChasing()
        {
            do
            {
                agent.speed = stats.runSpeed;
                if (observe.currentTarget != null)
                {
                    if (observe.CheckTarget_Distance_AndRaycast())
                    {
                        ChangeState(NpcState.Attack);
                        yield break;
                    }
                    else
                    {
                        agent.SetDestination(observe.currentTarget.position);
                    }
                }

                yield return new WaitForFixedUpdate();

            } while (true);
        }

        public float distanceForAttack = 0.5f;

        public int attackChance = 50;

        void Trigger_Attack_Anim()
        {
            if (!anim.GetBool("Block") && anim.GetInteger("Action") == -1)
            {
                ChooseAction();
            }
        }

        void ChooseAction()
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random < attackChance)
            {
                anim.SetInteger("Action", 0);
                anim.SetInteger("AttackInt", UnityEngine.Random.Range(0, 2));
            }
            else
            {
                anim.SetInteger("Action", 1);
            }
        }

        protected override void TurnOffBehaviour(NpcState prevState)
        {
            base.TurnOffBehaviour(prevState);
            if (prevState == NpcState.Attack)
            {
                rotate.enabled = false;
                anim.SetInteger("Action", -1);
            }
            else if (prevState == NpcState.Patrol)
            {
                StopAllCoroutines();
                rotate.enabled = false;
            }
        }

        public override void Attacked(Transform attacker)
        {
            base.Attacked(attacker);

            if (stats.currentHealth > 0)
                observe.currentTarget = attacker.transform;
        }

        public override void RecieveAttacked_InfoFromOtherNpc(Transform target, Transform attacker)
        {
            if (DoResponse())
            {
                foreach (string tag in stats.protects)
                {
                    if (tag == target.tag.ToString())
                        observe.currentTarget = attacker.transform;
                }
            }
        }

        bool DoResponse()
        {
            if (stats.protects.Count > 0 && observe.currentTarget == null)
            {
                return true;
            }

            return false;
        }

        protected override void OnDisable()
        {
            agent.stoppingDistance = 0.1f;
            anim.SetBool("Combat", false);
        }
    }
}
