using System;
using System.Collections.Generic;
using UnityEngine;

public class AiQueue : AIUnifiedTime
{
    [SerializeField] private int aiBudgetPerTick = 5;

    private readonly List<Action> aiActions = new List<Action>();
    private int currentIndex;

    public void Register(Action action)
    {
        if (!aiActions.Contains(action))
            aiActions.Add(action);
    }

    public void Unregister(Action action)
    {
        aiActions.Remove(action);
    }

    private void OnEnable() 
    {
        StartCoroutine(SecondsRoutine());
    }

    protected override void OnSecondsTick()
    {
        if (aiActions.Count == 0) return;
        Debug.Log("seconds");
        int processed = 0;

        while (processed < aiBudgetPerTick)
        {
            if (currentIndex >= aiActions.Count)
                currentIndex = 0;

            aiActions[currentIndex]?.Invoke();

            currentIndex++;
            processed++;

            if (processed >= aiActions.Count)
                break;
        }
    }
}