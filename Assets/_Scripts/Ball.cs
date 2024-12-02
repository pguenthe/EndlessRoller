using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float LateralSpeed = .1f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Running)
        {
            rb.AddForce(Vector3.right * Input.GetAxis("Horizontal"), ForceMode.Acceleration);
        }
    }
}
