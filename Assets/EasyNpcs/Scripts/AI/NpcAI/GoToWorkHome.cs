using System.Collections;
using UnityEngine;
using AIPackage;

namespace AIPackage
{
    public class GoToWorkHome : MonoBehaviour
    {
        NpcAI npcAI;

        public void Awake()
        {
            npcAI = GetComponent<NpcAI>();
        }

        public void Start_GOTOWork()
        {
            enabled = true;
            StartCoroutine(GoToWorkCoroutine());
        }

        IEnumerator GoToWorkCoroutine()
        {
            npcAI.agent.SetDestination(npcAI.work.position);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, npcAI.work.position) <= 0.5f);

            npcAI.ChangeState(NpcState.Working);
        }

        public void Start_GOTOHome()
        {
            enabled = true;
            StartCoroutine(GoHomeCoroutine());
        }

        IEnumerator GoHomeCoroutine()
        {
            npcAI.agent.SetDestination(npcAI.home.position);
            yield return new WaitUntil(() => npcAI.agent.remainingDistance <= 0.5f && !npcAI.agent.pathPending);

            npcAI.ChangeState(NpcState.AtHome);
        }

        private void OnEnable()
        {
            npcAI.agent.isStopped = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
