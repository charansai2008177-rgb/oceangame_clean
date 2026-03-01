using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    public class OnBlockAnimAI : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Block", true);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > 0.9f)
            {
                animator.SetBool("Block", false);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Block", false);
        }
    }
}
