using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    private Collider weaponCollider;
    public LayerMask whatIsPlayer;
    public EnemyManager enemyManager;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Player.instance.Die();
        }
    }

    public void OpenWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void CloseWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

}
