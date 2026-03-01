using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    [RequireComponent(typeof(NpcAI))]
    [RequireComponent(typeof(CombatAI))]
    public class AIChangeBuffer : MonoBehaviour
    {
        public bool default_IsNpc = true;
        public NpcBase enabledAI { get; private set; }

        NpcAI npc;
        CombatAI combat;

        private void Awake()
        {
            npc = GetComponent<NpcAI>();
            combat = GetComponent<CombatAI>();
        }

        private void Start()
        {
            if (default_IsNpc)
            {
                ChangeToNpc();
            }
            else
            {
                ChangeToCombat();
            }
        }

        public void ChangeToNpc()
        {
            npc.enabled = true;
            combat.enabled = false;

            enabledAI = npc;
        }

        public void ChangeToCombat()
        {
            npc.enabled = false;
            combat.enabled = true;

            enabledAI = combat;
        }

        public bool ChangeState(NpcState state)
        {
            if (state == NpcState.Scared)
                ChangeToNpc();

            return enabledAI.ChangeState(state);
        }

        public NpcState CurrentState()
        {
            return enabledAI.currentState;
        }
    }
}