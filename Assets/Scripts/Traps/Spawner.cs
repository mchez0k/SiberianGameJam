using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MovingTrap prefab;
    [SerializeField] private float spawnTime;
    private float currentSpawnTime;

    private void Awake()
    {
        currentSpawnTime = spawnTime;
    }
    private void Update()
    {
        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime >= 0) return;
        currentSpawnTime = spawnTime;
        Instantiate(prefab, transform.position, transform.rotation).Initialize(transform.forward);
    }
}
