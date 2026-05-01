using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed ;
    private Vector3 moveDir;
    private Rigidbody playerRB;

    // Reference to the child model
    public Transform characterModel;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.useGravity = false; // custom gravity handled elsewhere
        playerRB.constraints = RigidbodyConstraints.FreezeRotation; 
    }
     void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical");   // W/S
        moveSpeed = Player.Instance.playerMoveSpeed;

        
        // Local movement relative to orientation
        moveDir = (transform.right * h + transform.forward * v).normalized; 
    }

    void FixedUpdate()
    {
        if (moveDir.magnitude > 0.1f)
        {
            Vector3 targetPos = playerRB.position + moveDir * moveSpeed * Time.fixedDeltaTime;
            playerRB.MovePosition(targetPos);
        }
    }
}