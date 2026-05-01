using UnityEngine;

public class HexTransform : MonoBehaviour
{
    public Transform point;       // Reference point
    public float radius = 5f;     // Distance from the point

    private Vector3 direction;    // Direction away from the point

    void Start()
    {
        if (point != null)
        {
            // Initialize direction based on current position
            direction = (transform.position - point.position).normalized;
        }
    }

    void Update()
    {
        if (point != null)
        {
            // Position = point + direction * radius
            transform.position = point.position + direction * radius;
        }
    }
}
