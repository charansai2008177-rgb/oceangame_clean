using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    [CreateAssetMenu(fileName = "DialguePrefab.asset", menuName = "Dialogue")]
    public class DialoguePrefab : ScriptableObject
    {
        public Job jobNpc1;
        public Gender genderNpc1;
        public List<string> npc1;

        public Job jobNpc2;
        public Gender genderNpc2;
        public List<string> npc2;
    }
}