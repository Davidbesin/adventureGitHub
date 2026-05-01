using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerToMine), typeof(PlayerToDefensiveTower),
typeof(PlayerToSpend))]
public class Player : MonoBehaviour, IHealth
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static Player Instance {get; private set;}
    
    
    private int health;
    
    public float defaultMoveSpeed; 
    public float playerMoveSpeed; 
    private int MaxHealth;
    public int Health
    {
        get => health;
        private set => health = Mathf.Clamp(value, 0, MaxHealth);
    }
    
    
    public int GatherSpeed {get; private set;} = 100;

    private PlayerToMine myMineOperations;
    
    private PlayerToDefensiveTower myDefensiveTowerOperations;
    public List<BaseResource> playersInventory {get; set;} = new();

    
    private void Awake() 
    {
        Instance = this;
       
    }

    private void Start()
    {
         playerMoveSpeed = defaultMoveSpeed;
    }
    //Know the planet the player is on and collects data
    private Planet residingPlanet; 
    public Planet ResidingPlanet
    {
        get { return residingPlanet;}
        set { residingPlanet = value; }
    }
    
    public void CollectPlanet (Planet data)
    {
        ResidingPlanet = data; 
    }

    public void TakeDamage(int damage)
    {
        
    }
    public void SetMaxHealth(int playerMaxHealth)
    {
        MaxHealth = playerMaxHealth;
    }

    public void SetSpeed(float speed)
    {
        defaultMoveSpeed = speed;
    }

    public void SetGatherSpeed(int resourceGatherSpeed)
    {
        GatherSpeed = resourceGatherSpeed;
    }

    [SerializeField] bool stillSummoning;

    private void OnTriggerEnter(Collider other)
    {
       if (other.TryGetComponent(out Water water))
        {
            playerMoveSpeed = defaultMoveSpeed / 0.7f; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
       if (other.TryGetComponent(out Water water))
        {
            playerMoveSpeed = defaultMoveSpeed; 
        } 
    }
}
