using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AIPackage
{
    public class NpcAI : NpcBase
    {
        public float scaredRunningSpeed;
        public float runningDistance;
        public float runningTime;

        DayAndNightControl dayTimer;
        Work workScript;
        Behaviour homeScript;
        GoToWorkHome gotoWorkHome;
        Conversation runConversation;
        Rotate rotate;
        Observe senseTarget;

        public Transform home;
        public Transform work;

        TextMeshPro textMeshPro;

        protected override void Awake()
        {
            base.Awake();

            DayAndNightCycle_Initialize();
            workScript = GetComponent<Work>();
            homeScript = GetComponent<Home>();
            gotoWorkHome = GetComponent<GoToWorkHome>();
            run = GetComponent<RunFromDanger>();
            rotate = GetComponent<Rotate>();
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            runConversation = GetComponent<Conversation>();
            senseTarget = GetComponent<Observe>();
        }

        protected override void Start()
        {
            base.Start();

            currentCoolTime = 0;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            if (stats.weapon != null)
                stats.weapon.SetActive(false);
        }

        void DayAndNightCycle_Initialize()
        {
            dayTimer = FindObjectOfType<DayAndNightControl>();
            if (dayTimer != null)
            {
                dayTimer.OnMorningHandler += OnTimeOfDay;
                dayTimer.OnEveningHandler += OnTimeOfDay;
            }
        }

        float currentCoolTime = 0;

        void Update()
        {
            if (attacker == null)
            {
                Conversation_CoolTime();
            }
        }

        void Conversation_CoolTime()
        {
            currentCoolTime += Time.deltaTime;
            if (currentCoolTime >= converCoolTime)
            {
                TryConversation();
                currentCoolTime = 0;
            }
        }

        Transform attacker;

        public override void RecieveAttacked_InfoFromOtherNpc(Transform target, Transform attacker)
        {
            this.attacker = attacker;
            ChangeState(NpcState.Scared);
        }

        public NpcAI FindNpc_ForTalk()
        {
            List<NpcBase> npcs = senseTarget.FindNpcs_ForInteraction();
            NpcAI returnNpc = Return_RandomNpc(npcs);
            return returnNpc;
        }

        NpcAI Return_RandomNpc(List<NpcBase> npcs)
        {
            List<NpcAI> npcAIs = Return_NpcAIScripts(npcs);
            if (npcAIs.Count > 0)
            {
                return npcAIs[UnityEngine.Random.Range(0, npcAIs.Count)];
            }
            else
            {
                return null;
            }
        }

        List<NpcAI> Return_NpcAIScripts(List<NpcBase> npcs)
        {
            List<NpcAI> npcAIs = new List<NpcAI>();
            foreach (NpcBase npc in npcs)
            {
                if (npc is NpcAI)
                {
                    npcAIs.Add(npc as NpcAI);
                }
            }

            return npcAIs;
        }

        protected override void TurnOffBehaviour(NpcState prevState)
        {
            base.TurnOffBehaviour(prevState);
            switch (prevState)
            {
                case NpcState.GoingToWork:
                    gotoWorkHome.enabled = false;
                    break;

                case NpcState.GoingHome:
                    gotoWorkHome.enabled = false;
                    break;

                case NpcState.Working:
                    if (workScript != null)
                        workScript.WorkDisable();
                    break;

                case NpcState.AtHome:
                    if (homeScript != null)
                        homeScript.enabled = false;
                    break;

                case NpcState.Talking:
                    rotate.enabled = false;
                    textMeshPro.text = null;
                    runConversation.enabled = false;
                    break;

                case NpcState.Scared:
                    attacker = null;
                    break;
            }
        }

        protected override void OnIdle()
        {
            if (dayTimer != null)
            {
                OnTimeOfDay();
            }

            agent.speed = stats.walkSpeed;
        }

        void OnTimeOfDay()
        {
            if (enabled)
            {
                float time = dayTimer.currentTime;
                if (time > .3f && time < .7f)
                {
                    GoToWork();
                }
                else
                {
                    GoToHome();
                }
            }
        }

        void GoToWork()
        {
            ChangeState(NpcState.GoingToWork);
        }

        protected override void OnGoingToWork()
        {
            gotoWorkHome.Start_GOTOWork();
        }

        void GoToHome()
        {
            ChangeState(NpcState.GoingHome);
        }

        protected override void OnGoingToHome()
        {
            gotoWorkHome.Start_GOTOHome();
        }

        protected override void OnWorking()
        {
            if (workScript != null)
                workScript.enabled = true;
        }

        protected override void OnAtHome()
        {
            if (homeScript != null)
                homeScript.enabled = true;
        }

        public override void Attacked(Transform attacker)
        {
            base.Attacked(attacker);
            this.attacker = attacker;
            
            if (resistOnAttack)
            {
                ChangeToCombat(attacker);
            }
            else
            {
                ChangeState(NpcState.Scared);
            }
        }

        RunFromDanger run;

        protected override void OnScared()
        {
            run.enabled = true;
            if (!stats.isDead)
            {
                agent.speed = stats.runSpeed;
                StartCoroutine(run.Run());
            }
        }

        [Range(0, 10)]
        public int converFactor = 0;
        public int converCoolTime = 30;

        void TryConversation()
        {
            NpcAI nearbyNpc = FindNpc_ForTalk();
            if (nearbyNpc != null)
            {
                if (CheckConditions_ForTalk(nearbyNpc))
                {
                    Conversation runConversation = GetComponent<Conversation>();
                    runConversation.Set(nearbyNpc.transform, true);
                }
            }
        }

        bool CheckConditions_ForTalk(NpcAI npc)
        {
            if (npc.enabled == false)
                return false;
            if (currentState == NpcState.AtHome)
                return false;
            if (currentState == NpcState.Talking || npc.currentState == NpcState.Talking)
                return false;
            if (npc.currentState == NpcState.Scared)
                return false;
            if (UnityEngine.Random.Range(0, 10) > converFactor)
                return false;
            if (GetInstanceID() < npc.GetInstanceID())
                return false;

            return true;
        }

        public bool resistOnAttack = false;

        void ChangeToCombat(Transform attacker)
        {
            if (!forceEvent)
                senseTarget.currentTarget = attacker;
        }
    }
}