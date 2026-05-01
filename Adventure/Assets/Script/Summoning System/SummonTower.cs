using UnityEngine;

public class SummonTower : MonoBehaviour
{
    [SerializeField]GameObject tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void SummonAttackTower()
    {
        if (!SummonManager.Instance.NextAllowed) return;
        Instantiate(tower);
        SummonManager.Instance.AllowedStatus(false);
    }

}
