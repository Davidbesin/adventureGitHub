using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static TowerManager Instance {get; private set;}
    public List<BaseDefensiveTower> MyTowers = new();
    void Awake()
    {
        Instance = this;
    }

    public void JoinList(BaseDefensiveTower baseDefensiveTower)
    {
        MyTowers.Add(baseDefensiveTower);
        Debug.Log("Joined");
    }
    public void GetOutOfList(BaseDefensiveTower baseDefensiveTower)
    {
        MyTowers.Remove(baseDefensiveTower);
    }

    public void ChangeRange(float rang)
    {
        foreach(var tower in MyTowers)
        {
            tower.SetRange(rang);
        }
    }
}
