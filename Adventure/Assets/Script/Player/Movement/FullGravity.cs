using UnityEngine;

public class FullGravity : MonoBehaviour
{
    public GravityAttractor attractor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attractor.Attract(transform);
    }
}
