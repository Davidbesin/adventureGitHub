using UnityEngine;
using System.Collections;   
using System.Collections.Generic;

public class AttackDefensiveTower : BaseDefensiveTower
{
    Coroutine Attack;
    public float timeRate;
    [SerializeField] int type;
    [SerializeField]int damage;
    int Damage => type * damage; 
    Coroutine attack;
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
        if (enemyWithinRange.Count == 0) return ;
        BaseEnemyAI target = enemyWithinRange[0];
        if (target == null) return;
        target.TakeDamage(Damage);
    }
   
}
