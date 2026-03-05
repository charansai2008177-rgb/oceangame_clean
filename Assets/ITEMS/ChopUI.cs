using UnityEngine;
using UnityEngine.UI;

public class ChopUI : MonoBehaviour
{
    public Slider slider;

    public void UpdateBar(float progress)
    {
        slider.value = progress;
    }
}