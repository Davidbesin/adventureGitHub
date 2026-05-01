using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    // Separate events for each category
    public event Action AllMineSave;
    public event Action AllTowerSave;
    public event Action AllHexSave;

   


    private void Awake()
    {
        Instance = this;
    }

    // Hook these to UI Buttons
    public void SaveAllMines()
    {
        AllMineSave?.Invoke();
        Debug.Log("All mines saved.");
    }

    public void SaveAllTowers()
    {
        AllTowerSave?.Invoke();
        Debug.Log("All towers saved.");
    }

    public void SaveAllHexTiles()
    {
        AllHexSave?.Invoke();
        Debug.Log("All hex tiles saved.");
    }

    public void SaveAll()
    {
        AllMineSave?.Invoke();
        AllTowerSave?.Invoke();
        AllHexSave?.Invoke();
        Debug.Log("All mines, towers, and hex tiles saved.");
    }

   
}
