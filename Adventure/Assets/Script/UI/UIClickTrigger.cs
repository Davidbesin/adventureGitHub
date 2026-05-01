using UnityEngine;
using System;

public class UiClickTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Action OnUpgradeAction;

    // Called by the UI button
    public void UpgradeButtonClick()
    {
        OnUpgradeAction?.Invoke();
    }
}

