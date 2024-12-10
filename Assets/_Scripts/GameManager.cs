using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI DistanceText;
    public TextMeshProUGUI PointsText;
    public TextMeshProUGUI MultiplierText;
    public TextMeshProUGUI TimeText;
    public GameObject PlayAgainButton;
    public GameObject PlatformParent;
    public GameObject[] PlatformPrefabs;
    public float PlatformLength = 6;
    public int StartingPlatformCount = 8;
    public float PlatformOverlap = 0.1f;
    public int ShiftXEveryCount = 7;
    public int ShiftYEveryCount = 12;
    public int EasyPlatformPrefabCount;
    public int MediumPlatformPrefabCount;
    public int PlatformCountBeforeMedium = 10;
    public int PlatformCountBeforeHard = 18;
    public int FullPlatformsToStart = 2;

    public static float Speed = 7.8f; //2.2f;
    public static bool Running;

    private static float Multiplier = 1.0f;
    private static float MultiplierStartTime;
    private static float MultiplierDuration;

    //private float DistanceTraveled;
    private List<GameObject> Platforms;
    private float PlatformX = 0;
    private float PlatformY = 0;
    private int PlatformCount = 0;
    private float Points;
    private float StartTime;
    
    void Start()
    {
        Time.timeScale = 1.0f;
        //DistanceTraveled = 0;
        GameStatusText.gameObject.SetActive(false);
        PlayAgainButton.gameObject.SetActive(false);

        Platforms = new List<GameObject>();
        GameObject obj;

        for (int i = 0; i < FullPlatformsToStart; i++)
        {
            obj = Instantiate(PlatformPrefabs[0], new Vector3(0, 0, i * PlatformLength - PlatformOverlap), Quaternion.identity, PlatformParent.transform);
            Platforms.Add(obj);
            PlatformCount++;
        }

        for (int i = FullPlatformsToStart; i < StartingPlatformCount; i++)
        {
            int n = UnityEngine.Random.Range(0, EasyPlatformPrefabCount);
            obj = Instantiate(PlatformPrefabs[n], new Vector3(0, 0, i * PlatformLength - PlatformOverlap), Quaternion.identity, PlatformParent.transform);
            Platforms.Add(obj);
            PlatformCount++;
        }

        StartTime = Time.time;
        Running = true;
        Multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y <= PlatformY - 2.1)
        {
            Running = false;
            GameStatusText.text = "You\nDied";
            GameStatusText.gameObject.SetActive(true);
            PlayAgainButton.gameObject.SetActive(true);
        }

        if (Running)
        {
            if (Multiplier > 1)
            {
                CheckMultiplier();
            }

            ShowScore();

            //check position of first platform (0)
            if (Platforms[0].transform.position.z < Player.transform.position.z - .7 * PlatformLength)
            {
                GenerateNewPlatform();
            }
        }

    }

    private void GenerateNewPlatform()
    {
        //if it's far enough back
        //delete it from the scene and remove it from the list
        Destroy(Platforms[0]);
        Platforms.RemoveAt(0);

        //shift left or right every so often
        if (PlatformCount % ShiftXEveryCount == 0)
        {
            if (UnityEngine.Random.value <= .5f)
            {
                PlatformX += 1;
            }
            else
            {
                PlatformX -= 1;
            }
        }

        int rangeMax = PlatformPrefabs.Length;
        if (PlatformCount < PlatformCountBeforeMedium)
        {
            rangeMax = EasyPlatformPrefabCount;
        }
        else if (PlatformCount < PlatformCountBeforeHard)
        {
            rangeMax = EasyPlatformPrefabCount + MediumPlatformPrefabCount;
        }

        int n = UnityEngine.Random.Range(0, rangeMax);
        float zOffset = 0;

        if (PlatformCount % ShiftYEveryCount == 0)
        {
            n = 0;
            if (UnityEngine.Random.value <= .5f)
            {
                PlatformY += 1;
                zOffset = 0.7f;
            }
            else
            {
                PlatformY -= 1;
                zOffset = 0.4f;
            }
        }

        //add a new one to the end
        //figure out the position of the last one and add one length
        float z = Platforms[Platforms.Count - 1].transform.position.z + PlatformLength - PlatformOverlap;
        GameObject obj = Instantiate(PlatformPrefabs[n], new Vector3(PlatformX, PlatformY, z + zOffset), Quaternion.identity, PlatformParent.transform);
        Platforms.Add(obj);
        PlatformCount++;
    }

    private void ShowScore()
    {
        Points += Time.deltaTime * Speed * Multiplier;

        PointsText.text = Points.ToString("000000");
        DistanceText.text = Player.transform.position.z.ToString("00000.00") + "m";

        float timeSpent = Time.time - StartTime;
        int minutes = (int) timeSpent / 60;
        float seconds = timeSpent - minutes * 60;
        TimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00.00");

        float multiplierTimeRemaining = MultiplierStartTime + MultiplierDuration - Time.time;
        MultiplierText.text = $"{Multiplier}x";
        if (Multiplier > 1)
        {
            MultiplierText.text += $" - {multiplierTimeRemaining.ToString("00.00")}";
        }
    }

    public static void SetMultiplier(float multiplier, float duration)
    {
        if (Multiplier > 1)
        {
            if (Multiplier < multiplier)
            {
                Multiplier = multiplier;
            }
            MultiplierDuration += duration;
        }
        else
        {
            Multiplier = multiplier;
            MultiplierDuration = duration;
            MultiplierStartTime = Time.time;
        }
    }

    public void CheckMultiplier()
    {
        if (Time.time > MultiplierStartTime + MultiplierDuration)
        {
            Multiplier = 1;
        }
    }
}


