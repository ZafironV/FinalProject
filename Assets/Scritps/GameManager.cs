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
    public GameObject escapePrefab;
    public GameObject keyPrefab;
    public GameObject lifePrefab;
    public Player link;
    public int linkLives = 3;
    public string currentSceneName;
    public float enemySpawnInterval = 3f; // Tiempo de spawn para los enemigos
    public float keySpawnInterval = 20f; // Tiempo de spawn para las llaves
    public float lifeSpawnInterval = 10f; // Tiempo de spawn para las vidas
    public float gameDuration = 60f;
    private float timeRemaining;
    private float logInterval = 1f;
    private float timeSinceLastLog = 0f;
    public string loseSceneName = "LOSE";
    public float keyLifeTime = 5f; // Tiempo de vida de las llaves en la escena
    public float lifeLifeTime = 5f; // Tiempo de vida de las vidas en la escena

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        timeRemaining = gameDuration;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnKeys());
        StartCoroutine(SpawnLifes());
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
            yield return new WaitForSeconds(enemySpawnInterval);
            SpawnGoblin();
        }
    }

    IEnumerator SpawnKeys()
    {
        while (true)
        {
            yield return new WaitForSeconds(keySpawnInterval);
            SpawnKey();
        }
    }

    IEnumerator SpawnLifes()
    {
        while (true)
        {
            yield return new WaitForSeconds(lifeSpawnInterval);
            SpawnLife();
        }
    }

    void SpawnGoblin()
    {
        Transform spawnPoint = GetRandomSpawnPoint("SpawnPoint");
        Vector3 spawnPosition = spawnPoint.position;
        Instantiate(tentaclePrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnKey()
    {
        Transform spawnPoint = GetRandomSpawnPoint("KeySpawnPoint");
        Vector3 spawnPosition = spawnPoint.position;
        GameObject key = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        Destroy(key, keyLifeTime); // Destruir la llave después de 3 segundos
    }

    void SpawnLife()
    {
        Transform spawnPoint = GetRandomSpawnPoint("LifeSpawnPoint");
        Vector3 spawnPosition = spawnPoint.position;
        GameObject life = Instantiate(lifePrefab, spawnPosition, Quaternion.identity);
        LifeObject lifeObject = life.GetComponent<LifeObject>(); // Obtener componente LifeObject
        lifeObject.SetGameManager(this); // Pasar una referencia del GameManager al LifeObject
        Destroy(life, lifeLifeTime); // Destruir la vida después de 3 segundos
    }

    Transform GetRandomSpawnPoint(string tag)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(tag);
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
                Instantiate(escapePrefab, new Vector3(0, 0, 0), Quaternion.identity);
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

    public void IncreaseLinkLives()
    {
        linkLives++; // Incrementa las vidas del jugador
        UpdateHeartCount(); // Actualiza el texto de las vidas en la UI
    }

    void UpdateHeartCount()
    {
        heartCountText.text = "x " + linkLives.ToString();
    }
}
