using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace AIPackage
{
    public class Conversation : MonoBehaviour
    {
        bool isFirst;
        AIChangeBuffer buffer;
        Transform partner;
        Tuple<List<string>, List<string>> conversation = null;
        NavMeshAgent agent;
        TextMeshPro textMesh;
        Rotate rotate;

        public delegate void EventOnEnd();
        public event EventOnEnd convOnEnd;

        public bool recieveRequest = true;

        private void Awake()
        {
            rotate = GetComponent<Rotate>();
            agent = GetComponent<NavMeshAgent>();
            textMesh = GetComponentInChildren<TextMeshPro>();
            buffer = GetComponent<AIChangeBuffer>();
        }

        public void Set(Transform _partner, bool order = false, Tuple<List<string>, List<string>> conver = null)
        {
            enabled = true;
            isFirst = order;
            partner = _partner;
            partnerConv = _partner.GetComponent<Conversation>();
            conversation = conver;

            if (!isFirst)
                agent.isStopped = true;

            ChangeState_ToTalking();
        }

        void ChangeState_ToTalking()
        {
            if (!buffer.ChangeState(NpcState.Talking) || !partnerConv.recieveRequest)
            {
                enabled = false;
                if (!isFirst)
                {
                    partnerConv.EndConversation();
                }

                return;
            }

            rotate.RotateTo(partner.transform);
            MoveToStart();
        }

        void MoveToStart()
        {
            if (isFirst)
            {
                agent.SetDestination(Position_1FloatAway_FromPartner());
                StartConv();
            }
        }

        Vector3 Position_1FloatAway_FromPartner()
        {
            Vector3 myPos = transform.position;
            Vector3 partnerPos = partner.transform.position;
            float distance = Vector3.Distance(myPos, partnerPos);
            float distanceOffset = distance - 1;

            float desX = (myPos.x + partnerPos.x * distanceOffset) / distance;
            float desY = (myPos.y + partnerPos.y * distanceOffset) / distance;
            float desZ = (myPos.z + partnerPos.z * distanceOffset) / distance;

            return new Vector3(desX, desY, desZ);
        }

        Tuple<List<string>, List<string>> chosenConv;

        void StartConv()
        {
            chosenConv = Choose_Conversation();
            if (chosenConv != null)
            {
                StartCoroutine(Start_Talk());
            }
        }

        Tuple<List<String>, List<String>> Choose_Conversation()
        {
            if (conversation == null)
            {
                return ChooseConversation();
            }
            else
            {
                return conversation;
            }
        }

        Tuple<List<string>, List<string>> ChooseConversation()
        {
            AI_Stats meInfo = gameObject.GetComponent<AI_Stats>();
            AI_Stats partnerInfo = partner.GetComponent<AI_Stats>();
            Job[] jobs = { meInfo.job, partnerInfo.job };
            Gender[] genders = { meInfo.gender, partnerInfo.gender };

            return FindDialogue.GetDialgoue(genders, jobs);
        }

        bool lastTalker;
        Conversation partnerConv;

        IEnumerator Start_Talk()
        {
            partnerConv.Set(transform);
            StartCoroutine(ChangeText(chosenConv.Item1));
            yield return new WaitForSeconds(4);

            if (partnerConv != null)
            {
                FindLastSpeaker();
            }
            else
            {
                EndConversation();
            }
        }

        public IEnumerator ChangeText(List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                if (!text[i].StartsWith(" "))
                {
                    textMesh.text = text[i];
                    GetComponent<Animator>().SetTrigger("Talk");
                    yield return new WaitForSeconds(4);

                    if (i != text.Count - 1)
                    {
                        textMesh.text = null;
                        yield return new WaitForSeconds(4);
                    }
                }
            }

            StartCoroutine(IenumWrapperOfEndConv());
        }

        void FindLastSpeaker()
        {
            if (chosenConv.Item1.Count > chosenConv.Item2.Count)
            {
                lastTalker = true;
            }
            else
            {
                lastTalker = false;
            }

            partnerConv.RecieveRequest(chosenConv, !lastTalker);
        }

        void RecieveRequest(Tuple<List<string>, List<string>> chosenConv, bool lastTalker)
        {
            this.lastTalker = lastTalker;
            StartCoroutine(ChangeText(chosenConv.Item2));
        }

        IEnumerator IenumWrapperOfEndConv()
        {
            if (!lastTalker)
            {
                textMesh.text = null;
                yield return new WaitForSeconds(4);
            }

            EndConversation();
        }

        public void EndConversation()
        {
            if (convOnEnd != null)
                convOnEnd.Invoke();
            
            enabled = false;
        }

        private void OnDisable()
        {
            partnerConv.enabled = false;

            if (buffer.CurrentState() != NpcState.Scared)
                buffer.ChangeState(NpcState.Default);

            rotate.enabled = false;
            agent.isStopped = false;
            textMesh.text = null;

            StopAllCoroutines();
        }
    }
}