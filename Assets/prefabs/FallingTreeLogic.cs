using UnityEngine;

public class FallingTreeLogic : MonoBehaviour
{
    public float fallSpeed = 2f;
    private float AngleToFall = 90f;
    private float currentAngle = 0f;
    private Vector3 fallAxis;

    void Start()
    {
        // Fall away from the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = (transform.position - player.transform.position).normalized;
        // This calculates a side-axis to rotate around
        fallAxis = Vector3.Cross(Vector3.up, dir);
    }

    void Update()
    {
        if (currentAngle < AngleToFall)
        {
            float step = fallSpeed * (currentAngle + 10f) * Time.deltaTime;
            transform.RotateAround(transform.position, fallAxis, step);
            currentAngle += step;
        }
    }
}