using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GhostAbilities : MonoBehaviour
{
    [SerializeField] private Transform handPosition;
    [SerializeField] private List<Spell> spellPrefabs = new List<Spell>();
    [SerializeField] private List<Image> spellCooldowns = new List<Image>();
    public UnityEvent OnNewBlockAdded;

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

            if (Input.GetKeyDown(spell.GetSpellButton()) && spell.IsCanSpawn() && currentBlock is not null)
            {
                spell.ResetCooldown();

                Vector3 spawnDirection;

                if (Mathf.Abs(transform.forward.x) > Mathf.Abs(transform.forward.z))
                {
                    spawnDirection = transform.forward.z > 0 ? Vector3.forward : -Vector3.forward;
                }
                else
                {
                    spawnDirection = transform.forward.x > 0 ? Vector3.right : -Vector3.right;
                }

                Quaternion spawnRotation = Quaternion.LookRotation(spawnDirection);
                Vector3 spawnPosition = new Vector3(currentBlock.transform.position.x, handPosition.position.y, currentBlock.transform.position.z);
                Spell instance = Instantiate(spell, spawnPosition, spawnRotation).GetComponent<Spell>();
                instance.Spawn();

                if (instance.IsFaded)
                {
                    OnNewBlockAdded?.Invoke();
                }
            }
        }
    }
}
