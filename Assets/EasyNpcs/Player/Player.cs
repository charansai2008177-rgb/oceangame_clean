using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_Package;

namespace PlayerExample
{
    public class Player : MonoBehaviour
    {
        CharacterController controller;
        Camera playerCamera;
        TextAndButtons textAndButtons;
        CameraCon con;
        bool canMove;

        public KeyCode interactButton = KeyCode.E;

        // Start is called before the first frame update
        void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>();
            textAndButtons = FindObjectOfType<TextAndButtons>();
            con = GetComponentInChildren<CameraCon>();
            canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * 10 * Time.deltaTime);

                Interact();
            }
            else
            {
                ClickDialogue();
            }
        }

        void Interact()
        {
            if (Input.GetKeyDown(interactButton))
            {
                Vector3 cameraPos = playerCamera.transform.position;
                Vector3 rayDir = playerCamera.transform.forward;
                if (Physics.Raycast(cameraPos, rayDir, out RaycastHit hit,
                    2, 1 << 3))
                {
                    NpcInteract(hit.transform);
                }
            }
        }

        public void NpcInteract(Transform npcPart)
        {
            DialogueManager dialogue = npcPart.GetComponentInParent<DialogueManager>();
            if (dialogue.currentSentence != null)
            {
                PlayerControls_Switch(false);
                Cursor.lockState = CursorLockMode.None;
                textAndButtons.Start_Dialogue(dialogue);
                playerCamera.transform.LookAt(dialogue.transform.position + new Vector3(0, 1.6f, 0));
            }
        }

        void ClickDialogue()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!textAndButtons.AdvanceOnDialogue())
                {
                    textAndButtons.End_Dialogue();
                    PlayerControls_Switch(true);
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        public void PlayerControls_Switch(bool on)
        {
            canMove = on;
            con.enabled = on;
            Cursor.visible = !on;
        }
    }
}
