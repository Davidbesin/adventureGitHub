using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "PlayersInventory", menuName = "PlayersInventory", order = 0)]
public class PlayersInventory : ScriptableObject 
{
    [SerializeField] private List<BaseResource> playersResources = new();
    public List<BaseResource> PlayersResources 
    {
        get { return playersResources; }
        private set { playersResources = value; }
    }

    [Header("Inventory Gate")]
    public bool canAddResources = true;  

    public List<BaseResource> AccessInventory(Type baseResorces)
    {
        List<BaseResource> res = new();
        foreach (var p in playersResources)
        {
            if (baseResorces.IsAssignableFrom(p.GetType()))
            {
                res.Add(p);
            }
        }
        if (res.Count == 0) Debug.Log("Nothing works dawg");
        return res;
    }

    public void AddResources(Type type, int amountToAdd)
    {
        // Gate check
         if (!canAddResources) return;

         var resource = AccessInventory(type);
         foreach (var r in resource)
         {
            r.AddAmount(amountToAdd);
            Debug.Log($"Added {amountToAdd} to {r.name}, now {r.Amount}");
         }
    }

    public bool CanSubtract(Type type, int amountToSubtract)
    {
        var resource = AccessInventory(type);
        foreach (var r in resource)
        {
            if (r.Amount >= amountToSubtract)
            {
                return true; // Found enough of this resource
            }
        }
        return false; // None of the matching resources had enough
    }

    public bool SubtractResources(Type type, int amountToSubtract)
    {
        var resource = AccessInventory(type);
        foreach (var r in resource)
        {
            if (r.Amount < amountToSubtract) return false;
            r.SubtractAmount(amountToSubtract);
            return true;
        }
        return false;
    }
}
