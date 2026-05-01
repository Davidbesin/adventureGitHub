using UnityEngine;

public class RotateAndGetPlanetReady : MonoBehaviour
{
    [SerializeField] Vector3 angles;
    [SerializeField] Transform indicator;
    [SerializeField] float detectionRadius = 5f;

    void Start()
    {
        transform.eulerAngles = angles;
         transform.localScale = new Vector3(50f, 50f, 50f);
        CheckNearest();
    }

    

    void CheckNearest()
    {
        Collider[] hits = Physics.OverlapSphere(indicator.position, detectionRadius);

        foreach (Collider hit in hits)
        {
            Grid gridComponent = hit.GetComponent<Grid>();
            if (gridComponent != null)
            {
                Debug.Log("Touching object with Grid: " + hit.gameObject.name);
                // Do something with gridComponent here
            }
        }
    }

    // Draw gizmo in Scene view
    void OnDrawGizmos()
    {
        if (indicator != null)
        {
            Gizmos.color = Color.yellow; // choose any color
            Gizmos.DrawWireSphere(indicator.position, detectionRadius);
        }
    }
}
