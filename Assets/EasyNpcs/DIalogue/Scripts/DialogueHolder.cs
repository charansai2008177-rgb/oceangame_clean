using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue_Package
{
    [Serializable]
    public class DialogueEvent : UnityEvent<Sentence> { }

    public class DialogueHolder : MonoBehaviour
    {
        public Sentence currentSentence;
        public Sentence defaultSentence;

        void Awake()
        {
            currentSentence = defaultSentence;
        }

        public UnityEvent<Sentence> onDialogue_Close;
        public bool turnNpc = true;

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
    }
}