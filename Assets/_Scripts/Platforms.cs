using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Running)
        {
            gameObject.transform.position += Vector3.back * GameManager.Speed * Time.deltaTime;
        }
    }
}
