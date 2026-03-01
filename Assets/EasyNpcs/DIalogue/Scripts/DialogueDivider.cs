using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

namespace Dialogue_Package
{
    [IncludeInSettings(true)]
    public static class DialogueDivider 
    {
        public static Sentence DivideDialogue(Sentence givenSentence)
        {
            List<Sentence> dividedSentences = new List<Sentence>();
            string text = givenSentence.npcText;
            if (text != null)
            {
                int givenLength = text.Length;
                if (givenLength > 200)
                {
                    for (int i = 0; i < givenLength/200 + 1; i++)
                    {
                        Sentence dividedSentence = new Sentence();
                        int startString = i * 200;
                        if (startString + 200 > givenLength)
                        {
                            givenSentence.npcText = text.Substring(startString, givenLength - startString);
                            dividedSentences.LastOrDefault().nextSentence = givenSentence;
                        }
                        else
                        {
                            dividedSentence.npcText = text.Substring(startString, 200);
                            dividedSentences.Add(dividedSentence);
                            if (i > 0)
                            {
                                dividedSentences[i - 1].nextSentence = dividedSentence;
                            }
                        }
                    }
                    
                    return dividedSentences[0];
                }
            }

            return null;
        }
    }
}