using UnityEngine;
using System.Collections.Generic;

public abstract class BaseDefensiveTower : MonoBehaviour, IWaveContributor, IHealth
{
    public List<BaseEnemyAI> enemyWithinRange = new();

    [SerializeField] SummoningDecision state;


    [Header("Tower Stats")]
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private int baseRegenRateHealth = 1;
    [SerializeField] private float baseRange = 5f;
    [SerializeField] private int baseTowerLevel = 1;

    // --- Backing fields ---
    private int health;
    private int maxHealth;
    private int regenRateHealth;
    private float range;
    private int towerLevel;

    private SphereCollider trigger;
    private Planet planet;

    // --- Properties ---
    public int Health { get => health; private set => health = Mathf.Clamp(value, 0, MaxHealth); }
    public int MaxHealth => maxHealth;
    public int RegenRateHealth => regenRateHealth;
    public float Range => range;
    public int TowerLevel => towerLevel;

    // --- Upgradeable interfaces ---
    private UpgradeableStatInterface upgradeableHealth;
    private UpgradeableStatInterface upgradeableRegenHealth;
    private UpgradeableStatInterface upgradeableRange;
    private UpgradeableStatInterface upgradeableTowerLevel;

    public UpgradeableStatInterface UpgradeableHealth => upgradeableHealth;
    public UpgradeableStatInterface UpgradeableRegenHealth => upgradeableRegenHealth;
    public UpgradeableStatInterface UpgradeableRange => upgradeableRange;
    public UpgradeableStatInterface UpgradeableTowerLevel => upgradeableTowerLevel;

    private void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        if (!trigger || !trigger.isTrigger)
        {
            Debug.LogError("Tower requires a SphereCollider set as trigger.");
            return;
        }

        AddUpgradeInterfaces();
        ApplyAllStats();
    }

    private void Update()
    {
        Regenerate();
    }

    private void Regenerate()
    {
        if (Health < MaxHealth)
        {
            Health += Mathf.RoundToInt(regenRateHealth * Time.deltaTime);
            if (Health > MaxHealth) Health = MaxHealth;
        }
    }

    public void OnEnable()
    {
        ApplyAllStats();
        planet = Player.Instance?.ResidingPlanet;
        if (planet != null) planet.JoinList(this);

        if (TowerManager.Instance != null)
            TowerManager.Instance.JoinList(this);
        else
            Debug.LogError("TowerManager not ready when tower enabled!");
    }

    public void OnDisable()
    {
        if (planet != null) planet.GetOutOftheList(this);
        if (TowerManager.Instance != null) TowerManager.Instance.GetOutOfList(this);
    }

    public int ContributeToWave() => towerLevel;
    public void TakeDamage(int damage) => Health -= damage;

    protected virtual void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        if (!enemyWithinRange.Contains(enemy))
            enemyWithinRange.Add(enemy);

        DealWithEnemies();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        enemyWithinRange.Remove(enemy);
    }

    public void SetRange(float range)
    {
        this.range = range;
        if (trigger != null)
            trigger.radius = range;
    }

    protected abstract void DealWithEnemies();

    // --- Upgrade application ---
    public void ApplyStatsToMaxHealth() => maxHealth = baseHealth * upgradeableHealth.level;
    public void ApplyStatsToRegenHealth() => regenRateHealth = baseRegenRateHealth * upgradeableRegenHealth.level;
    public void ApplyStatsToRange() => range = baseRange * upgradeableRange.level;
    public void ApplyStatsToTowerLevel() => towerLevel = Mathf.Max(1, baseTowerLevel * upgradeableTowerLevel.level);

    private void ApplyAllStats()
    {
        ApplyStatsToMaxHealth();
        ApplyStatsToRegenHealth();
        ApplyStatsToRange();
        ApplyStatsToTowerLevel();
        Health = MaxHealth;
    }

    [ContextMenu("CreateUpgradeInterfaces")]
    private void AddUpgradeInterfaces()
    {
        upgradeableHealth = CreateInterface("Tower Health");
        upgradeableRegenHealth = CreateInterface("Tower Regen Health");
        upgradeableRange = CreateInterface("Tower Range");
        upgradeableTowerLevel = CreateInterface("Tower Level");
    }

    private UpgradeableStatInterface CreateInterface(string nameOfStats)
    {
        var upgrade = gameObject.AddComponent<UpgradeableStatInterface>();
        upgrade.ChangeTheString(nameOfStats);
        return upgrade;
    }

    public void LoadTower()
    {
        state.TowerIsPlaced();
    }
}
