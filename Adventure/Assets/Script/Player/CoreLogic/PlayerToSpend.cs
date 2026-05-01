using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerToSpend : MonoBehaviour
{
    ITransact currentTransaction;
    void OnTriggerEnter(Collider other)
    {   
        ITransact transact = other.GetComponent<ITransact>();
        currentTransaction = transact;
        if (transact == null) return;
        if (transact.AutoTransact)
        {   if (!currentTransaction.HasBought)
            {
                currentTransaction.DoYourTransaction();
            }
        }

        transact.toAllowBuy.Invoke();
  
    }

    void OnTriggerExit(Collider other)
    {   
        ITransact transact = other.GetComponent<ITransact>();
        if (transact == null) return;
        currentTransaction = null;
        transact.toNotAllowBuy.Invoke();
    }

    public void Transaction()
    {
        if (!currentTransaction.HasBought && !currentTransaction.AllowBuy)
        {
            currentTransaction.DoYourTransaction();
        }
        else{return;}
    }
}
