using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUi : MonoBehaviour
{
    public Image fillImage;

    public void UpdateFill(float value)
    {
        fillImage.fillAmount = value;
    }

    public void ResetFill()
    {
        fillImage.fillAmount = 0f;
    }

    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}