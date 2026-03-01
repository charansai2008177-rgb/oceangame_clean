using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    public class OnBlockImpact_Anim : StateMachineBehaviour
    {
        AudioSource audioSource;
        public AudioClip clip;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (audioSource == null)
            {
                audioSource = animator.GetComponent<AudioSource>();
            }

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}