using UnityEngine;
using System.Collections.Generic;
using System;

public class HexTransact : MonoBehaviour, ITransact
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Tooltip("Player inventory reference.")]
    public PlayersInventory playerBag;

    [Tooltip("Player second bigger inventory reference.")]
    public PlayersInventory playerVault;

    
    public TransactionSet toSpend = new();
    public bool HasBought { get; private set; }
    
    HexTile tile;
    
    public bool AutoTransact => true;
    public bool AllowBuy => true;
    public Action toAllowBuy => null;
    public Action toNotAllowBuy => null;
    void Start()
    {
        tile = GetComponent<HexTile>();
    }

    // Update is called once per frame
   

   public bool DoYourTransaction()
    {
        if (tile.Own) return true;
        if (toSpend == null) return false;

        List<TransactionItem> transactList = toSpend.items;

        // Phase 1: Check affordability
        foreach (var transact in transactList)
        {
            bool canPay = playerBag.CanSubtract(transact.resource.GetType(), transact.amount) ||
                        playerVault.CanSubtract(transact.resource.GetType(), transact.amount);

            if (!canPay)
            {
                Debug.Log("Transaction failed: insufficient resources");
                return false;
            }
        }

        // Phase 2: Commit deductions
        var deducted = new List<(Type type, int amount, string source)>();
        foreach (var transact in transactList)
        {
            if (playerBag.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                deducted.Add((transact.resource.GetType(), transact.amount, "bag"));
            }
            else if (playerVault.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                deducted.Add((transact.resource.GetType(), transact.amount, "vault"));
            }
            else
            {
                // Rollback if commit fails
                foreach (var d in deducted)
                {
                    if (d.source == "bag")
                        playerBag.AddResources(d.type, d.amount);
                    else
                        playerVault.AddResources(d.type, d.amount);
                }

                Debug.Log("Transaction failed: rollback complete");
                return false;
            }
        }

        Debug.Log("Upgrade transaction complete");
        HasBought = true;
        tile.SetBool(HasBought);
        return HasBought;
    }

}
