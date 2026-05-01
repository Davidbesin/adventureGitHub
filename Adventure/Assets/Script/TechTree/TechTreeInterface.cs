using UnityEngine;


public class TechTreeInterface : MonoBehaviour 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public float movementSpeed;
    public float baseTowerRange;
    public int playerMaxHealth;
    public int resourceGatherSpeed;
    /*public float carryCapacity;
    public float attackDamage;
    public float defense;
    public float craftingSpeed;
    public float towerDamage;
    public float towerFireRate;
    public float towerHealth;
 */
    private void Start()
    {
        ApplyStats();
    }
    void ApplyStats()
    {
        Player.Instance.SetSpeed(movementSpeed);
        Player.Instance.SetMaxHealth(playerMaxHealth);
        TowerManager.Instance.ChangeRange(baseTowerRange);
        Player.Instance.SetGatherSpeed(resourceGatherSpeed);
        /* Player.Instance.SetCarryCapacity(carryCapacity);
        Player.Instance.SetAttackDamage(attackDamage);
        Player.Instance.SetDefense(defense);
        Player.Instance.SetCraftingSpeed(craftingSpeed);

        
        TowerManager.Instance.SetDamage(towerDamage);
        TowerManager.Instance.SetFireRate(towerFireRate);
        TowerManager.Instance.SetHealth(towerHealth); */
    }
}

