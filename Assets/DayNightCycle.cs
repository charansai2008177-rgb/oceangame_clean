using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float dayDuration = 120f; // 2 minutes full cycle

    private float time;

    void Update()
    {
        time += Time.deltaTime / dayDuration;

        float sunAngle = time * 360f;
        sun.transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0);

        if (time >= 1f)
            time = 0f;
    }
}