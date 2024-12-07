using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject Target;
    //public float MaxSpeed = 1.0f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Target.transform.position;
    }

    void Update()
    {
        transform.position = Target.transform.position + offset;
        //Vector3 goal = Target.transform.position + offset;
        //transform.position = Vector3.MoveTowards(transform.position, goal, GameManager.Speed * Time.deltaTime);
        //transform.LookAt(Target.transform.position);
    }
}
