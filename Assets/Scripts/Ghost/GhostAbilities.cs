using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostAbilities : MonoBehaviour
{
    [SerializeField] private Transform handPosition;
    [SerializeField] private List<Spell> spellPrefabs = new List<Spell>();
    [SerializeField] private List<Image> spellCooldowns = new List<Image>();

    private Block currentBlock;

    private void Start()
    {
        for (int i = 0; i < spellPrefabs.Count; i++)
        {
            spellPrefabs[i].SetImage(spellCooldowns[i]);
        }
    }
    private void Update()
    {
        Debug.DrawRay(handPosition.position, Vector3.down * 2f);
        bool blockHit = Physics.Raycast(handPosition.position, Vector3.down, out RaycastHit rayInfo, 2f);
        Block detectedBlock = blockHit && rayInfo.collider.TryGetComponent(out Block block) ? block : null;

        HandleBlockSelection(detectedBlock);
        HandleSpells();
    }

    private void HandleBlockSelection(Block detectedBlock)
    {
        if (detectedBlock != currentBlock)
        {
            currentBlock?.Deselect();
            currentBlock = detectedBlock;
            currentBlock?.Select();
        }
    }

    private void HandleSpells()
    {
        foreach (Spell spell in spellPrefabs)
        {
            spell.DecreaseCooldown(Time.deltaTime);

            if (Input.GetKeyDown(spell.GetSpellButton()) && spell.IsCanSpawn(handPosition.position))
            {
                spell.ResetCooldown();
                Spell instance = Instantiate(spell, handPosition.position + spell.GetSpellOffset(), Quaternion.identity).GetComponent<Spell>();
                instance.Spawn();
            }
        }
    }
}
