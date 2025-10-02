using UnityEngine;

public class SwordAnimationEvents : MonoBehaviour
{
    private PlayerCombatManager playerCombatManager;

    private void Start()
    {
        playerCombatManager = GetComponentInParent<PlayerCombatManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AttackAnimationEvent()
    {
        if (playerCombatManager != null)
        {
            playerCombatManager.Attack();
        }
    }
}
