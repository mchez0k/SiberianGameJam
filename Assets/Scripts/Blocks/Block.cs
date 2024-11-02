using UnityEngine;

public class Block : MonoBehaviour
{
    [field: SerializeField] public EBlockType BlockType { get; private set; }
    private MeshRenderer mRenderer;
    private Material blockMaterial;
    private Color originalColor;
    [SerializeField] private Color selectEmissionColor = Color.yellow;
    [SerializeField] private float emissionIntensity = 1.0f;

    private void Awake()
    {
        mRenderer = GetComponentInChildren<MeshRenderer>();
        if (mRenderer != null)
        {
            blockMaterial = mRenderer.material;
            originalColor = blockMaterial.GetColor("_EmissionColor");
        }
    }

    public void Select()
    {
        SetEmission(selectEmissionColor, emissionIntensity);
    }

    public void Deselect()
    {
        SetEmission(originalColor, 0f);
    }

    private void SetEmission(Color color, float intensity)
    {
        if (blockMaterial != null)
        {
            Color finalColor = color * intensity;
            blockMaterial.SetColor("_EmissionColor", finalColor);
            blockMaterial.EnableKeyword("_EMISSION");
        }
    }
}