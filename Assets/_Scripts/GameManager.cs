using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlatformParent;
    public GameObject[] PlatformPrefabs;
    public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI DistanceText;
    public GameObject PlayAgainButton;
    public float PlatformLength = 6;
    public int StartingPlatforms = 4;

    public static float Speed = 7.5f;
    public static bool Running;

    private float DistanceTraveled;
    private List<GameObject> Platforms;

    void Start()
    {
        Running = true;
        DistanceTraveled = 0;
        GameStatusText.gameObject.SetActive(false);
        PlayAgainButton.gameObject.SetActive(false);

        Platforms = new List<GameObject>();

        GameObject obj = Instantiate(PlatformPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity, PlatformParent.transform);
        Platforms.Add(obj);

        for (int i = 1; i <= StartingPlatforms; i++)
        {
            int n = UnityEngine.Random.Range(0, PlatformPrefabs.Length - 1);
            obj = Instantiate(PlatformPrefabs[n], new Vector3(0, 0, i * PlatformLength), Quaternion.identity, PlatformParent.transform);
            Debug.Log($"Generated platform {n} at z={obj.transform.position.z}");
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
            DistanceTraveled += Time.deltaTime * Speed;
            DistanceText.text = DistanceTraveled.ToString("0000.00");

            if (Platforms[0].transform.position.z <= -1 * PlatformLength)
            {
                Destroy(Platforms[0]);
                Platforms.RemoveAt(0);

                int n = UnityEngine.Random.Range(0, PlatformPrefabs.Length - 1);
                float z = Platforms[Platforms.Count - 1].transform.position.z + PlatformLength;
                Platforms.Add(Instantiate(PlatformPrefabs[n], new Vector3(0, 0, z), Quaternion.identity, PlatformParent.transform));
            }
        }
    }
}
