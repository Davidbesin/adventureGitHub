using UnityEngine;

public class CanvasToCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera mainCamera;
    [SerializeField] Vector3 offset;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.transform.position + offset;
    }
}
