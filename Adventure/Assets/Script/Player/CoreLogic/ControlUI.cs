using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [SerializeField] private GameObject minePanel;
    [SerializeField] private GameObject towerPanel;

    private void Start()
    {
        minePanel.SetActive(false);
        towerPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mine>() != null)
        {
            minePanel.SetActive(true);
            towerPanel.SetActive(false);
        }
        else if (other.GetComponent<BaseDefensiveTower>() != null)
        {
            minePanel.SetActive(false);
            towerPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        minePanel.SetActive(false);
        towerPanel.SetActive(false);
    }
}