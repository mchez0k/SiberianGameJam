using UnityEngine;

public class GhostAbilities : MonoBehaviour
{
    [SerializeField] private GameObject firstSpellPrefab;
    [SerializeField] private float firstSpellCooldown;
    [SerializeField] private float currentFirstSpellCooldown;


    [SerializeField] private GameObject secondSpellPrefab;
    [SerializeField] private float secondSpellCooldown;
    [SerializeField] private float currentSecondSpellCooldown;

    [SerializeField] private Transform handPosition;

    private void Update()
    {
        DecreaseCooldowns();

        if (Input.GetKeyUp(KeyCode.Z) && currentFirstSpellCooldown <= 0)
        {
            currentFirstSpellCooldown = firstSpellCooldown;
            Instantiate(firstSpellPrefab, handPosition.position, Quaternion.identity);
        }

        if (Input.GetKeyUp(KeyCode.X) && currentSecondSpellCooldown <= 0)
        {
            currentSecondSpellCooldown = secondSpellCooldown;
            Instantiate(secondSpellPrefab, handPosition.position, Quaternion.identity);
        }
    }

    private void DecreaseCooldowns()
    {
        currentFirstSpellCooldown -= Time.deltaTime;
        currentSecondSpellCooldown -= Time.deltaTime;
    }
}
