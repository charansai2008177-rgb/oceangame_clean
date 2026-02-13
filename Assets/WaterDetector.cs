using UnityEngine;

public class WaterDetector : MonoBehaviour
{
    public bool isUnderwater;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isUnderwater = false;
        }
    }
}
