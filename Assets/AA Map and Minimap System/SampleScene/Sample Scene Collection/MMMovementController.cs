// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;

namespace AAMAP
{
    public class MMMovementController : MonoBehaviour
    {
        [Tooltip("The Character Controller component on the Player GameObject.")][SerializeField] private CharacterController characterController;
        [Tooltip("The speed of the player while walking.")][SerializeField] private float walkingSpeed = 25.0F;

        private Vector2 keyboardInput;
        private Vector3 applyVelocity;
        private float verticalVelocity = 0.0F;

        private void Update()
        {
            // Gets the keyboard input from the user.
            keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (characterController.isGrounded)
            {
                verticalVelocity = 0.0F;
            }

            verticalVelocity += 14.0F * Time.deltaTime;
            applyVelocity = (transform.forward * keyboardInput.y + transform.right * keyboardInput.x) * walkingSpeed + Vector3.down * verticalVelocity;

            // Moves the player.
            characterController.Move(applyVelocity * Time.deltaTime);
        }
    }
}
