using UnityEngine;

public class BaseNode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string techName;
    public Vector2 gridPosition; // logical position in tech tree

    public bool IsUnlocked { get; private set; }

    public void Unlock()
    {
        
            IsUnlocked = true;
            Debug.Log($"{techName} unlocked!");
    
    }
}


