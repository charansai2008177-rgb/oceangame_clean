using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace AIPackage
{
    public class NpcBase : MonoBehaviour
    {
        [HideInInspector]
        public NavMeshAgent agent;
        protected Animator anim;

        [HideInInspector]
        public TMP_Text textMesh;

        public NpcState currentState { get; protected set; }

        protected AI_Stats stats;

        bool _forceEvent;
        public bool forceEvent
        {
            get
            {
                return _forceEvent;
            }
            set
            {
                _forceEvent = value;
                ChangeState(NpcState.Default);
            }
        }

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            stats = GetComponent<AI_Stats>();
            forceEvent = false;
        }

        protected virtual void Start()
        {
            anim = GetComponentInChildren<Animator>();
            textMesh = GetComponentInChildren<TMP_Text>();
        }

        public virtual bool ChangeState(NpcState newState)
        {
            if (enabled && !forceEvent)
            {
                if (IsStageChangeAble(newState))
                {
                    StopAllCoroutines();

                    NpcState oldState = currentState;
                    currentState = newState;
                    OnStateChanged(oldState, newState);

                    return true;
                }
            }

            return false;
        }

        bool IsStageChangeAble(NpcState newState)
        {
            if (newState != NpcState.Default)
            {
                if (currentState == newState)
                    return false;

                return IsStateAdvantage_BiggerThan_Previous(newState);
            }

            return true;
        }

        bool IsStateAdvantage_BiggerThan_Previous(NpcState newState)
        {
            int currentStateAdvantage = StateAdvantageNumber(currentState);
            int newStateAdvantage = StateAdvantageNumber(newState);
            if (newStateAdvantage < currentStateAdvantage)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int StateAdvantageNumber(NpcState state)
        {
            switch (state)
            {
                case NpcState.Default:
                    return 0;

                case NpcState.GoingToWork:
                    return 1;

                case NpcState.Working:
                    return 1;

                case NpcState.GoingHome:
                    return 1;

                case NpcState.AtHome:
                    return 1;

                case NpcState.Talking:
                    return 2;

                case NpcState.Scared:
                    return 4;

                case NpcState.Patrol:
                    return 1;

                case NpcState.Chase:
                    return 3;

                case NpcState.Attack:
                    return 3;
            }

            return -1;
        }

        protected virtual void OnStateChanged(NpcState prevState, NpcState newState)
        {
            TurnOffBehaviour(prevState);
            switch (newState)
            {
                case NpcState.Default:
                    OnIdle();
                    break;

                case NpcState.GoingToWork:
                    OnGoingToWork();
                    break;

                case NpcState.Working:
                    OnWorking();
                    break;

                case NpcState.GoingHome:
                    OnGoingToHome();
                    break;

                case NpcState.AtHome:
                    OnAtHome();
                    break;

                case NpcState.Talking:
                    OnTalking();
                    break;

                case NpcState.Scared:
                    OnScared();
                    break;

                case NpcState.Patrol:
                    OnPatrol();
                    break;

                case NpcState.Chase:
                    OnChase();
                    break;

                case NpcState.Attack:
                    OnAttack();
                    break;
            }
        }

        protected virtual void TurnOffBehaviour(NpcState prevState) { }

        protected virtual void OnIdle() { }

        protected virtual void OnGoingToWork() { }

        protected virtual void OnGoingToHome() { }

        protected virtual void OnWorking() { }

        protected virtual void OnAtHome() { }

        protected virtual void OnTalking() { }

        protected virtual void OnScared() { }

        protected virtual void OnPatrol() { }

        protected virtual void OnChase() { }

        protected virtual void OnAttack() { }

        public virtual void Attacked(Transform attacker)
        {
            List<NpcBase> npcs = GetComponent<Observe>().FindNpcs_ForInteraction(false);
            if (npcs != null)
            {
                foreach (NpcBase npc in npcs)
                {
                    npc.RecieveAttacked_InfoFromOtherNpc(transform, attacker);
                }
            }
        }

        public virtual void RecieveAttacked_InfoFromOtherNpc(Transform target, Transform attacker) { }

        protected virtual void OnEnable()
        {
            ChangeState(NpcState.Default);
        }

        public void Disable()
        {
            enabled = false;
        }

        protected virtual void OnDisable()
        {
            TurnOffBehaviour(currentState);
            StopAllCoroutines();

            if (anim != null)
                anim.SetFloat("Speed", 0);
        }
    }
}