using UnityEngine;

public class CAMERA : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
