using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Dialogue_Package
{
    [IncludeInSettings(true)]
    [CreateAssetMenu(fileName = "Sentence.asset", menuName = "Sentence")]
    public class Sentence : ScriptableObject
    {
        public string playerText;
        public string npcText;

        public Sentence nextSentence;
        public List<Sentence> choices;
    }
}