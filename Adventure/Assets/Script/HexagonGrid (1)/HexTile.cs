using UnityEngine;
using System.Collections.Generic;
using System;

public class HexTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]int iD;
    public int ID => iD;

    [SerializeField]bool own;
    public bool Owned => own;
    public event Action OnChangeOwned;

    
    [SerializeField] HexManager hexManager;
    public bool Own
    {
        get => own; 
        private set
        {
            own = value;

            if (value)
            {
                hexManager.MoveToOwned(this);
            }
            else
            {
                hexManager.MoveToUnowned(this);
            } 
            
            OnChangeOwned?.Invoke();
        }
    }
    
    void Awake()
    {
        // Force the setter to run once at startup to sync lists and visibility
        SetBool(own); 
    }

    void Start()
    {
        OnChangeOwned?.Invoke();
    }
    public void SetBool(bool setBool)
    {
        Own = setBool;
    }

    public void AssignID(int theID)
    {
        iD = theID;
    }
}
