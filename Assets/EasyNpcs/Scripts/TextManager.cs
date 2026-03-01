using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace AIPackage
{
    public class TextManager : MonoBehaviour
    {
        void Start()
        {
            FindDialogue.allPrefabs = Resources.LoadAll<DialoguePrefab>("Conversations");
        }
    }

    public static class FindDialogue 
    {
        public static DialoguePrefab[] allPrefabs;

        public static Tuple<List<string>, List<string>> GetDialgoue(Gender[] genders = null, Job[] jobs = null)
        {
            List<DialoguePrefab> validDialogues = FindValidDialogues(genders, jobs);
            if (validDialogues.Count > 0)
            {
                var npc = validDialogues[UnityEngine.Random.Range(0, validDialogues.Count)];
                return new Tuple<List<string>, List<string>>(npc.npc1, npc.npc2);
            }

            return null;
        }

        static List<DialoguePrefab> FindValidDialogues(Gender[] genders, Job[] jobs)
        {
            genders = genders ?? new Gender[] { Gender.Default, Gender.Default };
            jobs = jobs ?? new Job[] { Job.Default, Job.Default };

            return Parse_DialogueTexts(genders, jobs);
        }

        static List<DialoguePrefab> Parse_DialogueTexts(Gender[] genders, Job[] jobs)
        {
            List<DialoguePrefab> validTexts = new List<DialoguePrefab>();
            foreach (var text in allPrefabs)
            {
                bool isValid = CheckFlags(genders, jobs, text);
                if (isValid)
                {
                    validTexts.Add(text);
                }
            }

            return validTexts;
        }

        static bool CheckFlags(Gender[] genders, Job[] jobs, DialoguePrefab text)
        {
            bool isValid = true;
            if (!jobs[0].HasFlag(text.jobNpc1) || !genders[0].HasFlag(text.genderNpc1))
            {
                isValid = false;
            }
            if (!jobs[1].HasFlag(text.jobNpc2) || !genders[1].HasFlag(text.genderNpc2))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}