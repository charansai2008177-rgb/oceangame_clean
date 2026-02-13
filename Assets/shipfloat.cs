using UnityEngine;

public class shipfloat : MonoBehaviour
{
    public float waterLevel = 1f;      // Exact Y of water surface
    public float floatHeight = 0.6f;   // How high ship sits
    public float bobSpeed = 1f;        // Wave speed
    public float bobAmount = 0.15f;    // Wave height

    public float tiltAmount = 1.5f;
    public float tiltSpeed = 0.8f;

    private float baseY;
    private Vector3 startRotation;

    void Start()
    {
        baseY = waterLevel + floatHeight;
        startRotation = transform.eulerAngles;
    }

    void Update()
    {
        BobAndFloat();
        Tilt();
    }

    void BobAndFloat()
    {
        float wave = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(
            transform.position.x,
            baseY + wave,
            transform.position.z
        );
    }

    void Tilt()
    {
        float tiltX = Mathf.Sin(Time.time * tiltSpeed) * tiltAmount;
        float tiltZ = Mathf.Cos(Time.time * tiltSpeed) * tiltAmount;

        transform.rotation = Quaternion.Euler(
            startRotation.x + tiltX,
            transform.eulerAngles.y,
            startRotation.z + tiltZ
        );
    }
}
