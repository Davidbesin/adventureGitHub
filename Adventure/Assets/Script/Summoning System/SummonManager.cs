using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class SummonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SummonManager Instance;
    public bool summoning;
    public Transform platform;
    [SerializeField] bool canEndSummoning; 
    [SerializeField] bool nextAllowed = true;
    public bool NextAllowed => nextAllowed;   
    
    public UnityEvent SummonEvents;
    public UnityEvent UnSummonEvents;

    void Awake()
    {
       if (Instance == null)
        Instance = this;
        else
        Destroy(gameObject);
    }  

    public void Summon()
    {
        summoning = true;
        SummonEvents?.Invoke();
    }

    public void UnSummon()
    {
        if (!canEndSummoning) return;
        summoning = false;
        UnSummonEvents?.Invoke();
    }

   public void CanEndSummoning()
   {
        canEndSummoning = true;
   }

   public void CannotEndSummoning()
   {
        canEndSummoning = false;
   }

   public void AllowedStatus(bool boole)
   {
        nextAllowed = boole;
   }
}
