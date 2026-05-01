using UnityEngine;
using System;
// This script acts as a proxy for the real transaction
public class TransactAndUnlockHex : MonoBehaviour, ITransact
{
    private HexTransact realTransaction;
     HexTileSensor sensor;
    public bool AutoTransact => true;
    public bool AllowBuy => true;
    public Action toAllowBuy => null;
    public Action toNotAllowBuy => null;
    
    // Interface Implementation: It just points to the real transaction's status
    public bool HasBought => realTransaction != null && realTransaction.HasBought;

    void Awake()
    {
        sensor = GetComponent<HexTileSensor>();
        realTransaction = sensor.neighbors[0].GetComponent<HexTransact>();
    }

    public bool DoYourTransaction()
    {
        if (realTransaction == null) return false;

        // 1. Call the method on the other script
        bool success = realTransaction.DoYourTransaction();

        // 2. If it worked, do the extra unlock logic here
        return success;
    }
}
