using UnityEngine;
using System.Collections.Generic;

public class MoveSlowlyDefensiveTower : BaseDefensiveTower
{
    public int enemySpeed;
    int baseSpeed;
    public UpgradeableStatInterface upgradeableStatInterface;

    protected override void DealWithEnemies()
    {
        foreach (var enemy in enemyWithinRange)
        {
            enemy.moveSpeed = enemySpeed;
        }
    }

    public void SetSpeed()
    {
        enemySpeed = baseSpeed / upgradeableStatInterface.level;
    }

}
