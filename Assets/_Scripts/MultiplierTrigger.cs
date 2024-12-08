using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierTrigger : MonoBehaviour
{
    public float Multiplier = 2;
    public float Duration = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.SetMultiplier(Multiplier, Duration);
            Destroy(gameObject);
        }
    }
}
