using UnityEngine;
using UnityEngine.UI;

namespace Dialogue_Package
{
    public class TextAndButtons : MonoBehaviour
    {
        [HideInInspector]
        public GameObject mainTextUI;
        [HideInInspector]
        public GameObject[] buttons;
        DialogueManager npcDialogue;

        private void Awake()
        {
            mainTextUI = transform.GetChild(0).gameObject;

            buttons = new GameObject[4];
            buttons[0] = transform.GetChild(1).gameObject;
            buttons[1] = transform.GetChild(2).gameObject;
            buttons[2] = transform.GetChild(3).gameObject;
            buttons[3] = transform.GetChild(4).gameObject;
        }

        public void Start_Dialogue(DialogueManager given_Manager)
        {
            npcDialogue = given_Manager;
            ChangeText(npcDialogue.GetNpcText());

            mainTextUI.SetActive(true);
            npcDialogue.enabled = true;
        }

        public void End_Dialogue()
        {
            mainTextUI.SetActive(false);
            npcDialogue.enabled = false;
        }

        public Text textComponent;

        public void ChangeText(string npcText)
        {
            textComponent.text = npcText;
        }

        public void Change_To_NextSentence()
        {
            npcDialogue.currentSentence = npcDialogue.GetNextSentence();
            ChangeText(npcDialogue.GetNpcText());
        }

        public void Disable_Text_For_Choices()
        {
            mainTextUI.SetActive(false);
            Activate_Choices();
        }

        void Activate_Choices()
        {
            int choiceNum = 0;
            foreach (GameObject button in buttons)
            {
                if (npcDialogue.GetListOfChoices().Count > choiceNum)
                {
                    button.SetActive(true);
                    button.GetComponentInChildren<Text>().text = npcDialogue.GetListOfChoices()[choiceNum].playerText;
                }
                else
                {
                    break;
                }

                choiceNum++;
            }
        }

        public void PressButton(int i)
        {
            Disable_Buttons();

            npcDialogue.currentSentence = npcDialogue.GetListOfChoices()[i];
            ChangeText(npcDialogue.GetNpcText());
        }

        void Disable_Buttons()
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }

            mainTextUI.SetActive(true);
        }

        public bool AdvanceOnDialogue()
        {
            Sentence sentence = npcDialogue.currentSentence;
            if (sentence.nextSentence != null)
            {
                Change_To_NextSentence();
                return true;
            }
            else if (sentence.choices != null)
            {
                Disable_Text_For_Choices();
                return true;
            }

            return false;
        }
    }
}