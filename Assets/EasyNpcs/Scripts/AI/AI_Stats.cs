using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    public class AI_Stats : Stats
    {
        public float walkSpeed = 2;
        public float runSpeed = 4;

        public float visionRange = 10;
        public float visionAngle = 25f;
        
        public enum Weapon { melee};
        public Weapon assignedWeapon;

        public Job job;
        public Gender gender;

        public List<string> enemies;
        public List<string> protects;

        public GameObject weapon;

        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = LayerMask.NameToLayer("Npc");
        }
    }
}

