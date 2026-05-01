using UnityEngine;
using System.Collections;

public class GoldStat : UpgradeableStatInterface
{

    private static int _staticLevel = 1;
    public static int staticlevel
    {
        get => _staticLevel;
        set => _staticLevel = Mathf.Max(1, value); // clamp to minimum of 1
    }



    private void OnEnable()
    {
        // Start the registration coroutine
        StartCoroutine(RegisterWithManager());
    }

    private IEnumerator RegisterWithManager()
    {
        int attempts = 0;
        while (attempts < 5)
        {
            if (MiningRateManager.Instance != null)
            {
                if (!MiningRateManager.Instance.goldList.Contains(this))
                {
                    MiningRateManager.Instance.goldList.Add(this);
                    Debug.Log($"GoldStat registered successfully on attempt {attempts + 1}, level={staticlevel}");

                    MiningRateManager.Instance.SyncTransactions();
                }
                yield break; 
            }

            attempts++;
            Debug.LogWarning($"GoldStat registration attempt {attempts} failed (manager not ready). Retrying...");
            yield return new WaitForSeconds(0.2f); // short delay before retry
        }

        Debug.LogError("GoldStat failed to register after 5 attempts.");
    }

    private void OnDisable()
    {
        if (MiningRateManager.Instance != null)
        {
            MiningRateManager.Instance.goldList.Remove(this);
        }
    }
}
