using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    public static bool Paused;

    // Start is called before the first frame update
    void Start()
    {
        //hide the menu on start
        Menu.SetActive(false);
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Paused)
            {
                Time.timeScale = 0.0f;
                GameManager.Running = false;

                Menu.SetActive(true);
                Paused = true;
            } 
            else
            {
                Time.timeScale = 1.0f;
                GameManager.Running = true;

                Menu.SetActive(false);
                Paused = false;
            }
        }
    }
}
