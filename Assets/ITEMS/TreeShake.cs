using UnityEngine;

public class TreeShake : MonoBehaviour
{
    private Vector3 originalPos;
    public float shakeAmount = 0.1f;

    void Start() { originalPos = transform.position; }

    public void TriggerShake()
    {
        // This stops any current shake and starts a new one
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    System.Collections.IEnumerator ShakeRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            yield return new WaitForSeconds(0.05f);
        }
        transform.position = originalPos; // Reset
    }
}