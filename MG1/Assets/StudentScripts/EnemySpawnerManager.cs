using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private List<GameObject> activeEnemies;
    [SerializeField] private GameObject enemyPrefab;
    public Collider triggerSpawnCollider;
    public static EnemySpawnerManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("More than one enemy spawner");
        }
    }

    private void Start()
    {
        ResetEnemies();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void AddActiveEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void ResetEnemies()
    {
        triggerSpawnCollider.gameObject.SetActive(true);
        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }
            
        activeEnemies.Clear();
        foreach (Transform spawn in spawnPoints)
        {
           Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
        }
    }

}
