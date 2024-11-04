using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [field: SerializeField] public EBlockType BlockType { get; private set; }
    public Collider[] blocks;
    private MeshRenderer mRenderer;
    private Material blockMaterial;
    private Color originalColor;
    [SerializeField] private Color selectEmissionColor = Color.yellow;
    [SerializeField] private float emissionIntensity = 1.0f;

    private void Awake()
    {
        CalculateBlocks();
        mRenderer = GetComponentInChildren<MeshRenderer>();
        if (mRenderer != null)
        {
            blockMaterial = mRenderer.material;
            originalColor = blockMaterial.GetColor("_EmissionColor");
        }
        //FindObjectOfType<GhostAbilities>().OnNewBlockAdded.AddListener(CalculateBlocks);
    }

    private void CalculateBlocks()
    {
        BlocksCleaning(Physics.OverlapSphere(transform.position, 0.75f, LayerMask.GetMask("Block")));
    }

    private void BlocksCleaning(Collider[] detectedBlocks)
    {
        List<Collider> filteredBlocks = new List<Collider>();

        foreach (var collider in detectedBlocks)
        {
            Block block = collider.GetComponent<Block>();

            if (collider.gameObject == gameObject || block == null || block.BlockType == EBlockType.Obstacle) continue;

            filteredBlocks.Add(collider);
        }

        blocks = filteredBlocks.ToArray();
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