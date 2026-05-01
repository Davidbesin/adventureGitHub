using UnityEngine;
using System.Collections;   
using System.Collections.Generic;

public class AreaOfEffectDefensiveTower : BaseDefensiveTower
{
    Coroutine Attack;
    public float timeRate;
    [SerializeField]float baseTimeRate;

    
    [SerializeField]int baseDamage;
    int Damage; 
    Coroutine attack;

    [SerializeField] UpgradeableStatInterface upgradeableTime;
    [SerializeField] UpgradeableStatInterface upgradeableAttack;
    protected override void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        Debug.Log("Entered mytarap" );

        if (!enemyWithinRange.Contains(enemy))  {enemyWithinRange.Add(enemy);} 
        if (attack == null) {attack= StartCoroutine(AttackRunning(timeRate));}
       
    }

    // Enemy leaves range
    protected override void OnTriggerExit(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        
        if (enemy == null) return;

        enemyWithinRange.Remove(enemy);
        if (enemyWithinRange.Count == 0 && attack != null)  {StopCoroutine(attack); attack = null;}
       
    }

   IEnumerator AttackRunning(float seconds)
    {
        while (enemyWithinRange.Count > 0)
        {
            DealWithEnemies();
            yield return new WaitForSeconds(seconds);
        }
    }

    protected override void DealWithEnemies()
    {    
        foreach (BaseEnemyAI enemy in enemyWithinRange)
        {
            if (enemy == null)
            continue;
            enemy.TakeDamage(Damage);
        }
    }
   
   public void ApplyStatsToDamage ()
    {
        Damage = upgradeableAttack.level * baseDamage;
    }
    public void ApplyStatsToTimeRate()
    {
        timeRate = baseTimeRate * upgradeableTime.level;
    }
}
