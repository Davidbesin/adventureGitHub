using UnityEngine;

[CreateAssetMenu(fileName = "BaseResource", menuName = "BaseResource", order = 0)]
public class BaseResource : ScriptableObject 
{
    [SerializeField] private int amount;
    public string NumberAsString => amount.ToString();
    
    public int Amount 
    { 
        get { return amount; } 
        private set { amount = Mathf.Max(0, value); }  // clamp to 0
    }

    public void AddAmount(int addition)  
    {
        Amount += addition;
        Debug.Log($"Added {addition}, new amount = {amount}");
    }

    public void SubtractAmount(int subtraction) 
    {
        // Clamp so it never goes below 0
        Amount -= subtraction;
        if (Amount < 0) Amount = 0;

        Debug.Log($"Subtracted {subtraction}, new amount = {amount}");
    }
}
