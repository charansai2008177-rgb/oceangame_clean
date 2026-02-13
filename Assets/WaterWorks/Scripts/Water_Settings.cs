using UnityEngine;

[ExecuteAlways]
public class Water_Settings : MonoBehaviour
{
    [SerializeField] private Material waterVolume;
    [SerializeField] private Material waterMaterial;

    Renderer rend;

    void OnEnable()
    {
        Initialize();
        UpdateWater();
    }

    void OnValidate()
    {
        Initialize();
        UpdateWater();
    }

    void Initialize()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (waterVolume == null)
            waterVolume = Resources.Load<Material>("Water_Volume");

        if (waterMaterial == null && rend != null)
            waterMaterial = rend.sharedMaterial;
    }

    void UpdateWater()
    {
        if (waterVolume == null || waterMaterial == null || rend == null)
            return;

        float boundsY = rend.bounds.size.y / -2f;

        float waterY =
            boundsY
            + transform.position.y
            + (waterMaterial.GetFloat("_Displacement_Amount") / 3f);

        waterVolume.SetVector("pos", new Vector4(0, waterY, 0, 0));
    }
}
