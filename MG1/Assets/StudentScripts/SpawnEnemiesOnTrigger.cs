using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesOnTrigger : MonoBehaviour
{
    public List<Transform> spawnLocations = new();
    public GameObject enemyPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform location in spawnLocations)
            {
                Instantiate(enemyPrefab, location.position, Quaternion.identity);
                gameObject.SetActive(false);
            }

        }
    }
}
