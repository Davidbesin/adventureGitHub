using UnityEngine;

[RequireComponent(typeof(MineAppearanceController))]
[RequireComponent(typeof(MineSave))]
public class Mine : MonoBehaviour, IHealth
{
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private float baseRegenRate = 0.1f;
    [SerializeField] private int baseMaxAmount = 50;
    [SerializeField] private float baseRegenRateHealth;
    [SerializeField] private BaseResource resourceType;
    [SerializeField] private Animator billboard;

    public BaseResource ResourceType => resourceType;

    public int Health { get => (int)healthf; private set => healthf = value; }
    public int MineAmount { get => (int)mineAmountF; private set => mineAmountF = value; }
    public int MineMaxAmount => mineMaxAmount;
    public float RegenRate => regenRate;
    public float RegenRateHealth => regenRateHealth;

    private float healthf;
    private float mineAmountF;
    [SerializeField] private int mineAmountInspector;
    [SerializeField] private int mineMaxAmount;
    [SerializeField] private float regenRate;
    [SerializeField] private float regenRateHealth;

    private UpgradeableStatInterface upgradeableRegenRate;
    private UpgradeableStatInterface upgradeableMaxAmount;
    private UpgradeableStatInterface upgradeablehealth;
    private UpgradeableStatInterface upgradeableRegenhealth;

    public UpgradeableStatInterface UpgradeableRegenRate => upgradeableRegenRate;
    public UpgradeableStatInterface UpgradeableMaxAmount => upgradeableMaxAmount;
    public UpgradeableStatInterface Upgradeablehealth => upgradeablehealth;
    public UpgradeableStatInterface UpgradeableRegenhealth => upgradeableRegenhealth;


    private void Awake()
    {
        MineAmount = mineMaxAmount;
        Health = baseHealth;
        AddUpgradeInterFace();
    }

    private void Update() => Regenerate();

    private void Regenerate()
    {
        if (MineAmount < mineMaxAmount)
        {
            mineAmountF += regenRate * Time.deltaTime;
            if (MineAmount > mineMaxAmount)
                mineAmountF = mineMaxAmount;
        }
        mineAmountInspector = MineAmount;
    }

    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(0, Health - damage);
        if (Health == 0)
        {
            // destruction logic
        }
    }

    public int Collect(int requestedAmount)
    {
        int collected = Mathf.Min(requestedAmount, MineAmount);
        MineAmount -= collected;
        Debug.Log($"Mine.Collect: requested={requestedAmount}, collected={collected}, remaining={MineAmount}");
        return collected;
    }

    private void OnTriggerEnter(Collider other) => billboard?.SetBool("Appear", true);
    private void OnTriggerExit(Collider other) => billboard?.SetBool("Appear", false);

    // Upgrade application
    public void ApplyStatsToHealth() => Health = baseHealth * upgradeablehealth.level;
    public void ApplyStatsToRegenRate() => regenRate = baseRegenRate * upgradeableRegenRate.level;
    public void ApplyStatsToMaxAmount() => mineMaxAmount = baseMaxAmount * upgradeableMaxAmount.level;
    public void ApplyStatsToRegenHealth() => regenRateHealth = baseRegenRateHealth * upgradeableRegenhealth.level;

    [ContextMenu("CreateTheInterface")]
    private void AddUpgradeInterFace()
    {
        upgradeableRegenRate = CreateInterface("Regenerate Resources");
        upgradeableMaxAmount = CreateInterface("Mine Max Amount");
        upgradeablehealth = CreateInterface("Mine Health");
        upgradeableRegenhealth = CreateInterface("Regeneration health");
    }

    private UpgradeableStatInterface CreateInterface(string nameOfStats)
    {
        var upgrade = gameObject.AddComponent<UpgradeableStatInterface>();
        upgrade.ChangeTheString(nameOfStats);
        return upgrade;
    }

    public void AssignResource(BaseResource resource)
    {
        resourceType = resource;
    }

}
