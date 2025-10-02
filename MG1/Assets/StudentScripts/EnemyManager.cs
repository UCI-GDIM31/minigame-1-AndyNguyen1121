using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private NavMeshAgent agent;
    public ParticleSystem hitParticle;
    public Animator weaponAnimator;
    public bool canAttack = true;
    public float attackRange = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        EnemySpawnerManager.instance.AddActiveEnemy(gameObject);
    }

    private void Update()
    {
        HandleEnemyBehavior();
        HandleEnemyAttacks();
    }

    private void HandleEnemyBehavior()
    {
        if (Player.instance == null || agent == null)
        {
            Debug.Log("Player or NavMesh not initilized");
        }

        agent.SetDestination(Player.instance.transform.position);
    }

    public void DestroyEnemy()
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        EnemySpawnerManager.instance.RemoveEnemy(gameObject);
        Player.instance.AddEnemyPoint();
        Destroy(gameObject);
    }

    public void HandleEnemyAttacks()
    {
        if (Vector3.Distance(Player.instance.transform.position, transform.position) <= attackRange && canAttack) 
        {
            canAttack = false;
            weaponAnimator.CrossFade("AxeSwing", 0.1f);
        }
    }
}
