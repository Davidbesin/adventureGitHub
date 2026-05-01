using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerTokenTransaction : MonoBehaviour, ITransact
{
    public List<TransactionItem> spendRequirements = new();
    public static List<TowerToken> towerTokenList = new();
    public bool HasBought { get; private set; }
    public bool AutoTransact => false;

    public bool AllowBuy { get; private set; }

    public Action toAllowBuy => AllowBuyToggleOn;
    public Action toNotAllowBuy => AllowBuyToggleOff;

    [Tooltip("Player inventory reference.")]
    public PlayersInventory playerBag;

    [Tooltip("Player second bigger inventory reference.")]
    public PlayersInventory playerVault;

    public void BuyTowerToken()
    {
        if (!AllowBuy) return;
        if (DoYourTransaction())
            towerTokenList.Add(new TowerToken());
    }

    public bool DoYourTransaction()
    {
        foreach (var transact in spendRequirements)
        {
            if (!playerBag.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                if (!playerVault.SubtractResources(transact.resource.GetType(), transact.amount)) return false;
            }
        }

        Debug.Log("transaction complete");
        HasBought = true;
        return HasBought;
    }

    private void OnTriggerEnter(Collider other)
    {
        TokenShop tokenShop = other.GetComponent<TokenShop>();
        if (tokenShop == null) return;
    }

    private void OnTriggerExit(Collider other)
    {
        TokenShop tokenShop = other.GetComponent<TokenShop>();
        if (tokenShop == null) return;
    }

    public void AllowBuyToggleOn() => AllowBuy = true;
    public void AllowBuyToggleOff() => AllowBuy = false;

    //  Bool method to remove a token
    public bool RemoveTowerToken(TowerToken token)
    {
        if (towerTokenList.Contains(token))
        {
            towerTokenList.Remove(token);
            Debug.Log("TowerToken removed successfully.");
            return true;
        }
        Debug.LogWarning("Attempted to remove a TowerToken that wasn't in the list.");
        return false;
    }

    // Debug in Update showing how many tokens exist
    private void Update()
    {
        //Debug.Log($"TowerToken count: {towerTokenList.Count}");
    }
}
