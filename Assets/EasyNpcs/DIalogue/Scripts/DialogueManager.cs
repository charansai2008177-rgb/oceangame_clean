using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Unity.VisualScripting;
using AIPackage;
using Dialogue_Package;

namespace Dialogue_Package
{
    [IncludeInSettings(true)]
    public class DialogueManager : MonoBehaviour
    {
        public Sentence currentSentence;
        public Sentence defaultSentence;
        public Transform player;

        NpcAI npcAI;
        CombatAI combatAI;
        NavMeshAgent agent;

        Rotate rotate;
        AimRig aimRig;

        void Awake()
        {
            npcAI = GetComponent<NpcAI>();
            combatAI = GetComponent<CombatAI>();
            agent = GetComponent<NavMeshAgent>();

            rotate = GetComponent<Rotate>();
            aimRig = GetComponent<AimRig>();
        }

        private void OnEnable()
        {
            npcAI.enabled = false;
            combatAI.enabled = false;
            agent.SetDestination(transform.position);

            rotate.RotateTo(player);
            aimRig.RigToTarget(player);
        }

        public UnityEvent<Sentence> onDialogue_Close;
        public bool turnNpc = true;

        protected virtual void OnDisable()
        {
            rotate.enabled = false;
            aimRig.RemoveRig();

            EnableAI();

            onDialogue_Close.Invoke(currentSentence);
            currentSentence = defaultSentence;
        }

        void EnableAI()
        {
            if (turnNpc)
            {
                npcAI.enabled = true;
            }
            else
            {
                combatAI.enabled = true;
            }
        }

        public string GetNpcText()
        {
            return currentSentence.npcText;
        }

        public Sentence GetNextSentence()
        {
            return currentSentence.nextSentence;
        }

        public List<Sentence> GetListOfChoices()
        {
            return currentSentence.choices;
        }

        public void SetQuestSentence(Sentence sentence)
        {
            currentSentence = sentence;
            defaultSentence = sentence;
        }
    }
}
