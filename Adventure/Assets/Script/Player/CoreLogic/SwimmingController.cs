using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwimmingController : MonoBehaviour
{
    public float swimSpeed = 5f;       // How fast the player swims
    public float buoyancyForce = 10f;  // Upward force in water
    public float waterDrag = 2f;       // Resistance in water

    private Rigidbody rb;
    private bool isInWater;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isInWater)
        {
            // Get input
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float upDown = 0f;

            if (Input.GetKey(KeyCode.Space)) upDown = 1f;   // swim upward
            if (Input.GetKey(KeyCode.LeftShift)) upDown = -1f; // swim downward

            Vector3 swimDirection = new Vector3(h, upDown, v);
            rb.AddForce(swimDirection * swimSpeed, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        if (isInWater)
        {
            // Apply buoyancy
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);

            // Apply drag
            rb.linearDamping = waterDrag;
        }
        else
        {
            rb.linearDamping = 0f; // normal drag outside water
        }
    }

    // Detect water trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
        }
    }
}
