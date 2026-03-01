using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    public class AttackHandler : MonoBehaviour
    {
        Observe observe;
        Transform attacker;
        AIChangeBuffer buffer;
        Stats stats;

        private void Awake()
        {
            observe = GetComponent<Observe>();
            buffer = GetComponent<AIChangeBuffer>();
            stats = GetComponent<Stats>();
        }

        public void ReactToAttack(Transform _attacker)
        {
            attacker = _attacker;

            NpcBase npc = buffer.enabledAI;
            if (npc != null)
            {
                npc.Attacked(_attacker);
            }
               
            Broadcast_BeingAttacked();
        }

        void Broadcast_BeingAttacked()
        {
            List<NpcBase> nearbyNpcs = observe.FindNpcs_ForInteraction(false);
            if (nearbyNpcs != null)
            {
                foreach (NpcBase nearbyNpc in nearbyNpcs)
                {
                    nearbyNpc.RecieveAttacked_InfoFromOtherNpc(transform, attacker);
                }
            }
        }

        public void SendAttack(Transform attacker)
        {
            DealDamage(attacker.GetComponent<Stats>());
            ReactToAttack(attacker);
        }

        void DealDamage(Stats attacker)
        {
            float baseDamage = attacker.damage;

            baseDamage -= stats.armour;
            if (baseDamage < 0)
                baseDamage = 0;

            stats.currentHealth = stats.currentHealth - baseDamage;
        }
    }
}
