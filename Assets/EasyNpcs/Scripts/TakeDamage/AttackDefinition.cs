using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIPackage;

namespace AttackDef
{
    [CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
    public class AttackDefinition : ScriptableObject
    {
        [Range(1.5f, 10f)]
        public float Cooldown;

        public float minDamage;
        public float maxDamage;
        public float criticalMultipliyer;
        public float criticalChance;
        public float Range;

        public Attack CreateAttack(AI_Stats attacker, AI_Stats defender)
        {
            float baseDamage = attacker.damage;

            if (defender != null)
                baseDamage -= defender.armour;

            if (baseDamage < 0)
                baseDamage = 0;
            return new Attack((int)baseDamage);
        }
    }
}
