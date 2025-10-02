using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCombatManager : MonoBehaviour
{

    private bool canAttack = true;
    public Animator swordAnimator;
    private StarterAssetsInputs _input;
    private Camera mainCam;

    [Header("Attack Attributes")]
    public LayerMask whatIsEnemy;
    public float attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.attack && canAttack)
        {
            _input.attack = false;
            canAttack = false;
            swordAnimator.CrossFade("PlayerSwordSlash", 0.1f); 
        }
        else if (_input.attack)
        {
            _input.attack = false;
        }
    }

    public void ResetAttack()
    {
        canAttack = true;
    }

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, attackRange, whatIsEnemy))
        {
            EnemyManager enemyManager = null;
            if (hit.collider.TryGetComponent<EnemyManager>(out enemyManager))
            {
                enemyManager.DestroyEnemy();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mainCam != null)
            Gizmos.DrawRay(mainCam.transform.position, mainCam.transform.forward * attackRange);
    }
}
