using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIPackage
{
    public class AIMovementAnim : MonoBehaviour
    {
        Animator anim;
        NavMeshAgent agent;

        void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
    }
}