using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text timeRemainingText;
    public Image heartImage;
    public Text heartCountText;
    public GameObject tentaclePrefab;
    public Player link;
    public int linkLives = 3;
    public string currentSceneName;
    public float spawnInterval = 3f;
    public float gameDuration = 5f;
    private float timeRemaining;
    private float logInterval = 1f;
    private float timeSinceLastLog = 0f;
    public string winSceneName = "WIN";
    public string loseSceneName = "LOSE";

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        timeRemaining = gameDuration;
        StartCoroutine(SpawnEnemies());
        UpdateHeartCount();
    }

    void Update()
    {
        UpdateTimer();
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnGoblin();
        }
    }

    void SpawnGoblin()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        Vector3 spawnPosition = spawnPoint.position;
        Instantiate(tentaclePrefab, spawnPosition, Quaternion.identity);
    }

    Transform GetRandomSpawnPoint()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].transform;
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeSinceLastLog += Time.deltaTime;

            if (timeSinceLastLog >= logInterval)
            {
                timeRemainingText.text = Mathf.Ceil(timeRemaining).ToString();
                timeSinceLastLog = 0f;
            }

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                SceneManager.LoadScene(winSceneName);
            }
        }
    }

    public void EnemyTouchLink()
    {
        linkLives = Mathf.Max(0, linkLives - 1);
        if (linkLives <= 0)
        {
            SceneManager.LoadScene(loseSceneName);
        }
        else
        {
            UpdateHeartCount();
        }
    }

    void UpdateHeartCount()
    {
        heartCountText.text = "x " + linkLives.ToString();
    }
}

