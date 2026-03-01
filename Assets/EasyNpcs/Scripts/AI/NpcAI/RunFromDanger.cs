using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using AIPackage;

namespace AIPackage
{
    public class RunFromDanger : MonoBehaviour
    {
        NavMeshAgent agent;
        NpcAI npc;

        private float runTimeLeft = 10;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            npc = GetComponent<NpcAI>();

            runTimeLeft = npc.runningTime;
        }

        private void Update()
        {
            runTimeLeft -= Time.deltaTime;
        }

        public IEnumerator Run()
        {
            while (runTimeLeft > 0)
            {
                ChooseAndSetDestination();
                yield return new WaitUntil(() => Vector3.Distance(agent.destination, transform.position) <= 0.5f);
            }

            runTimeLeft = npc.runningTime;
            npc.ChangeState(NpcState.Default);
            enabled = false;
        }

        void ChooseAndSetDestination()
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 15;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 15, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);
        }
    }
}
