using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float LateralForce = .1f;
    public float JumpForce = 6.0f;
    public float Radius = 0.3f;
    public float RaycastTolerance = 0.05f;
    public float AirborneSteerStrength = 0.6f;
    public GameObject GroundSparkEffect;
    public Vector3 GroundSparkAngle;

    private Rigidbody RB;
    private bool Grounded;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Running)
        {
            RaycastHit hit;
            bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit,
                Radius + RaycastTolerance, 1);

            if (!Grounded & grounded)
            {
                Debug.Log("Hit!");
                GameObject sparks = Instantiate(GroundSparkEffect,
                    transform.position + Vector3.down * Radius,
                    Quaternion.Euler(GroundSparkAngle)
                );
                Destroy(sparks, 0.1f);
            }
            Grounded = grounded;

            if (Grounded)
            {
                RB.AddForce(Vector3.right * Input.GetAxis("Horizontal"), ForceMode.Acceleration);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                }

                //RB.AddForce(Vector3.forward * GameManager.Speed * Time.deltaTime, ForceMode.Impulse);
                Vector3 vel = RB.velocity;
                vel.z = GameManager.Speed;
                RB.velocity = vel;
            }
            else
            {
                RB.AddForce(Vector3.right * Input.GetAxis("Horizontal") * AirborneSteerStrength,
                    ForceMode.Acceleration);
            }
        }
    }

}
