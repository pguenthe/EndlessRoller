using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI DistanceText;

    public static float Speed = 5;
    public static bool Running;

    private float DistanceTraveled;

    void Start()
    {
        Running = true;
        DistanceTraveled = 0;
        GameStatusText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y <= -1)
        {
            Running = false;
            GameStatusText.text = "You\nDied";
            GameStatusText.gameObject.SetActive(true);
        }

        if (Running)
        {
            DistanceTraveled += Time.deltaTime * Speed;
            DistanceText.text = DistanceTraveled.ToString("0000.00");
        }
    }
}