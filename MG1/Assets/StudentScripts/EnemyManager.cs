using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleEnemyBehavior();
    }

    private void HandleEnemyBehavior()
    {
        if (Player.instance == null || agent == null)
        {
            Debug.Log("Player or NavMesh not initilized");
        }

        agent.SetDestination(Player.instance.transform.position);
    }
}
