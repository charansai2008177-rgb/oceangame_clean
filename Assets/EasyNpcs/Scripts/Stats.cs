using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AIPackage
{
    public class Stats : MonoBehaviour
    {
        public float maxHealth;
        float _currentHealth;
        public float currentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = value;

                onDamage?.Invoke();
                CheckDeath();
            }
        }

        void CheckDeath()
        {
            if (_currentHealth <= 0 && !isDead)
            {
                isDead = true;
                onDeath?.Invoke();
            }
        }

        public float damage;
        public float armour;
        public float attackDistance = 1;

        [SerializeField]
        float _attackSpeed;
        public float attackSpeed
        {
            get { return _attackSpeed; }
            set
            {
                _attackSpeed = value;
                GetComponentInChildren<Animator>().SetFloat("AttackSpeed", _attackSpeed);
            }
        }

        public UnityEvent onDamage;
        public UnityEvent onDeath;
        public bool isDead = false;

        protected virtual void Awake()
        {
            _currentHealth = maxHealth;
            attackSpeed = _attackSpeed;
        }
    }
}
