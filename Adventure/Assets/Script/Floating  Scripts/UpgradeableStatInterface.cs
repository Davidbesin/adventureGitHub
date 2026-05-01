using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class UpgradeableStatInterface : MonoBehaviour, ITransact
{
    [Header("Name of stats")]
    [SerializeField] string nameOfStats;

    [Tooltip("Current upgrade level.")]
    [SerializeField] public int level = 1;

    public bool AutoTransact => false;
    public bool AllowBuy => true;
    public Action toAllowBuy => null;
    public Action toNotAllowBuy => null;
    [Tooltip("component that receives the stat upgrade. Choose")]

    //event
    public UnityEvent OnUpgradeStats;

   // public enum UpgradeStats

    [Tooltip("Player inventory reference.")]
    public PlayersInventory playerBag;

    [Tooltip("Player second bigger inventory reference.")]
    public PlayersInventory playerVault;

    [Header("Unlocks & Transactions")]
    private int CurrentIndex => level - 1;
    public List<TransactionSet> toSpend = new();
    public bool HasBought { get; private set; }

    [Header("On Enter and Exit Trigger. Suscribe and unsuscribe methods")]
    [SerializeField]UiClickTrigger uiClickTrigger;


    private void OnEnable()
    {
        ApplyLevelScaling();
    }

    
    public void ApplyLevelScaling()
    {
        OnUpgradeStats?.Invoke();
    }

    public bool DoYourTransaction()
    {
        if (toSpend == null || toSpend.Count <= CurrentIndex) return false;

        List<TransactionItem> transactList = toSpend[CurrentIndex].items;

        foreach (var transact in transactList)
        {
            if (!playerBag.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                 if (!playerVault.SubtractResources(transact.resource.GetType(), transact.amount)) return false;
            }
        }

        Debug.Log("Upgrade transaction complete");
        HasBought = true;
        return HasBought;
    }

    public void UpgradeLevel()
    {
        if (!DoYourTransaction()) return;
        level++;
        ApplyLevelScaling();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        Debug.Log("Player entered upgrade trigger zone");
        uiClickTrigger.OnUpgradeAction += UpgradeLevel;
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        Debug.Log("Player exited upgrade trigger zone");
        uiClickTrigger.OnUpgradeAction -= UpgradeLevel;
    }

    public void ChangeTheString(string name){nameOfStats = name;}

    public void AssignLevel(int lvlSent)
    {
        level = lvlSent;
    }
    public enum AvailableStats{MineRegenrate, MineHealth, MineMaxAmount, DefensiveTowerHealth}
}
