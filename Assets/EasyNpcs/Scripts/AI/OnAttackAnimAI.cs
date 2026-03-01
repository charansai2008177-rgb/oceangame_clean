using UnityEngine;
using UnityEngine.AI;

namespace AIPackage
{
    public class OnAttackAnimAI : StateMachineBehaviour
    {
        AI_Stats stats;
        Observe sense;
        Transform transform;
        Animator anim;

        bool trigger = true;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            anim = animator;
            GetRequiredComponents();
            trigger = true;
        }

        void GetRequiredComponents()
        {
            if (transform == null)
            {
                transform = anim.transform;
                stats = anim.GetComponent<AI_Stats>();
                sense = anim.GetComponent<Observe>();
            }

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > 0.7f && trigger)
            {
                if (stats != null)
                {
                    trigger = false;
                    Attack();
                }
            }
        }

        void Attack()
        {
            if (stats.assignedWeapon == AI_Stats.Weapon.melee)
            {
                ExecuteAttack();
            }
        }

        void ExecuteAttack()
        {
            Transform target = sense.currentTarget;
            Animator targetAnim = target.GetComponent<Animator>();

            if (targetAnim != null)
            {
                if (targetAnim.GetBool("Block"))
                {
                    anim.SetTrigger("BlockImpact");
                }
                else
                {
                    if (1 << target.gameObject.layer == 1 << 3)
                    {
                        target.GetComponent<AttackHandler>().SendAttack(transform);
                    }
                    else
                    {
                        target.GetComponent<Stats>().currentHealth -= stats.damage;
                    }

                    AttackEffect(targetAnim);
                }
            }
        }

        void AttackEffect(Animator targetAnim)
        {
            targetAnim.SetTrigger("Impact");
        }
    }
}