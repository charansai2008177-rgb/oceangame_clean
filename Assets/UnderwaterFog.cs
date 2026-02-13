using UnityEngine;

public class UnderwaterFog : MonoBehaviour
{
    [Header("References")]
    public WaterDetector waterDetector;

    [Header("Fog Settings")]
    public Color underwaterFogColor = new Color(0f, 0.35f, 0.5f);
    public float underwaterFogDensity = 0.04f;

    private Color defaultFogColor;
    private float defaultFogDensity;
    private bool defaultFogState;

    void Start()
    {
        defaultFogState = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
    }

    void Update()
    {

        if (waterDetector != null && waterDetector.isUnderwater)
        {
            Debug.Log("UNDERWATER");
        }

        if (waterDetector != null && waterDetector.isUnderwater)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterFogColor;
            RenderSettings.fogDensity = underwaterFogDensity;
        }
        else
        {
            RenderSettings.fog = defaultFogState;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
        }
    }
}
