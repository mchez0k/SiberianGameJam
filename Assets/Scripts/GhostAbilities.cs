using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostAbilities : MonoBehaviour
{
    [SerializeField] private Transform handPosition;
    [SerializeField] private List<Spell> spellPrefabs = new List<Spell>();
    [SerializeField] private List<Image> spellCooldowns = new List<Image>();

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
        for (int i = 0; i < spellPrefabs.Count; i++)
        {
            spellPrefabs[i].DecreaseCooldown(Time.deltaTime);

            if (Input.GetKeyDown(spellPrefabs[i].GetSpellButton()) && spellPrefabs[i].IsCanSpawn(handPosition.position))
            {
                spellPrefabs[i].ResetCooldown();
                Instantiate(spellPrefabs[i], handPosition.position + spellPrefabs[i].GetSpellOffset(), Quaternion.identity).GetComponent<Spell>().Spawn();
            }
        }
    }
}
