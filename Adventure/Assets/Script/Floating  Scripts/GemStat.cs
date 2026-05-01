using UnityEngine;
using System.Collections;

public class GemStat : UpgradeableStatInterface
{

    private static int _staticLevel = 1;
    public static int staticlevel
    {
        get => _staticLevel;
        set => _staticLevel = Mathf.Max(1, value); // clamp to minimum of 1
    }

    private void OnEnable()
    {
        StartCoroutine(RegisterWithManager());
    }

    private IEnumerator RegisterWithManager()
    {
        int attempts = 0;
        while (attempts < 5)
        {
            if (MiningRateManager.Instance != null)
            {
                if (!MiningRateManager.Instance.gemList.Contains(this))
                {
                    MiningRateManager.Instance.gemList.Add(this);
                    Debug.Log($"GemStat registered successfully on attempt {attempts + 1}, level={staticlevel}");

                    MiningRateManager.Instance.SyncTransactions();
                }
                yield break;
            }

            attempts++;
            Debug.LogWarning($"GemStat registration attempt {attempts} failed (manager not ready). Retrying...");
            yield return new WaitForSeconds(0.2f);
        }

        Debug.LogError("GemStat failed to register after 5 attempts.");
    }

    private void OnDisable()
    {
        if (MiningRateManager.Instance != null)
        {
            MiningRateManager.Instance.gemList.Remove(this);
        }
    }
}
