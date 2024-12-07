using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI DistanceText;
    public GameObject PlayAgainButton;
    public GameObject PlatformParent;
    public GameObject[] PlatformPrefabs;
    public float PlatformLength = 6;
    public int StartingPlatformCount = 8;
    public float PlatformOverlap = 0.3f;

    public static float Speed = 7.8f; //2.2f;
    public static bool Running;

    private float DistanceTraveled;
    private List<GameObject> Platforms;


    void Start()
    {
        //Time.timeScale = 1.0f;
        Running = true;
        DistanceTraveled = 0;
        GameStatusText.gameObject.SetActive(false);
        PlayAgainButton.gameObject.SetActive(false);

        Platforms = new List<GameObject>();
        GameObject obj = Instantiate(PlatformPrefabs[0], Vector3.zero, Quaternion.identity, PlatformParent.transform);
        Platforms.Add(obj);

        for (int i = 1; i < StartingPlatformCount; i++)
        {
            int n = UnityEngine.Random.Range(0, Platforms.Count);
            obj = Instantiate(PlatformPrefabs[n], new Vector3(0, 0, i * PlatformLength - PlatformOverlap), Quaternion.identity, PlatformParent.transform);
            Platforms.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y <= -1)
        {
            Running = false;
            GameStatusText.text = "You\nDied";
            GameStatusText.gameObject.SetActive(true);
            PlayAgainButton.gameObject.SetActive(true);
        }

        if (Running)
        {
            //DistanceTraveled += Time.deltaTime * Speed;
            DistanceTraveled = Player.transform.position.z;
            DistanceText.text = DistanceTraveled.ToString("00000.00");

            //check position of first platform (0)
            if (Platforms[0].transform.position.z < Player.transform.position.z -.7 * PlatformLength)
            {
                //if it's far enough back
                //delete it from the scene and remove it from the list
                Destroy(Platforms[0]);
                Platforms.RemoveAt(0);

                //add a new one to the end
                int n = UnityEngine.Random.Range(0, Platforms.Count);
                //figure out the position of the last one and add one length
                float z = Platforms[Platforms.Count - 1].transform.position.z + PlatformLength - PlatformOverlap;
                GameObject obj = Instantiate(PlatformPrefabs[n], new Vector3(0, 0, z), Quaternion.identity, PlatformParent.transform);
                Platforms.Add(obj);
            }
        }

    }
}
