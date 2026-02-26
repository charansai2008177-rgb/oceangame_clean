// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;

namespace AAMAP
{
    public class MMMouseController : MonoBehaviour
    {
        [Tooltip("The Player GameObject.")][SerializeField] private GameObject Player;
        [Tooltip("The speed of the Player's rotation by the mouse input.")][SerializeField] private float mouseSensitivity = 150.0F;

        //This variable is going to the be used while clamping the camera rotation
        float horizontalRotation = 0F;

        private void LateUpdate()
        {
            //Gets the user's mouse input
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);

            //Inverts the vertical mouse control
            horizontalRotation -= mouseInput.y;

            //Clamps the camera's horizontal rotation value
            horizontalRotation = Mathf.Clamp(horizontalRotation, -90F, 90F);

            //Applies the Y axis rotation to the Player GameObject
            Player.transform.Rotate(Vector3.up, mouseInput.x);

            //Applies the X axis rotation to the camera object
            transform.localRotation = Quaternion.Euler(horizontalRotation, 0F, 0F);
        }
    }
}
