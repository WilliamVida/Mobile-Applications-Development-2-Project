using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int playerScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int enemyCountPerWave = 20;
    [SerializeField] private TextMeshProUGUI enemyRemainingText;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    private int enemeyRemainingCount;
    public int waveNumber = 1;
    int bossWave = 4;
    private SoundController sc;
    bool bossExits;
    public GameObject boss;

    private void Start()
    {
        UpdateScore();
        enemeyRemainingCount = enemyCountPerWave;
        sc = SoundController.FindSoundController();
    }

    void Update()
    {
        if (bossExits = true && !GameObject.FindGameObjectWithTag("EnemyBoss") && (waveNumber % bossWave == 0))
        {
            enemyCountPerWave += 20;
            waveNumber++;
            waveNumberText.text = waveNumber.ToString();
            Invoke("EnableSpawning", 5);
            bossExits = false;
        }
    }

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.EnemyKilledEvent += OnEnemyKilledEvent;
        EnemyBossHealth.EnemyKilledEvent += OnEnemyBossKilledEvent;
        PointSpawners.EnemySpawnedEvent += PointSpawners_OnEnemySpawnedEvent;
    }

    private void OnDisable()
    {
        EnemyHealth.EnemyKilledEvent -= OnEnemyKilledEvent;
        EnemyBossHealth.EnemyKilledEvent -= OnEnemyBossKilledEvent;
        PointSpawners.EnemySpawnedEvent -= PointSpawners_OnEnemySpawnedEvent;
    }

    private void PointSpawners_OnEnemySpawnedEvent()
    {
        enemeyRemainingCount--;
        UpdateEnemeyRemaining();

        if (enemeyRemainingCount == 0)
        {
            DisableSpawning();
            StartCoroutine(SetupNextWave());
        }
    }

    private void UpdateEnemeyRemaining()
    {
        enemyRemainingText.text = enemeyRemainingCount.ToString("");
    }

    private IEnumerator SetupNextWave()
    {
        yield return new WaitForSeconds(7.5f);
        enemeyRemainingCount = enemyCountPerWave;
        waveNumber++;
        enemyCountPerWave += 40;
        waveNumberText.text = waveNumber.ToString("0");
        EnableSpawning();
    }

    private void DisableSpawning()
    {
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    private void EnableSpawning()
    {
        if ((waveNumber % bossWave == 0) && !GameObject.FindGameObjectWithTag("EnemyBoss"))
        {
            GameObject instance = Instantiate(boss);
            bossExits = true;
        }
        else
        {
            foreach (var spawner in FindObjectsOfType<PointSpawners>())
            {
                spawner.EnableSpawning();
            }
        }
    }

    private void OnEnemyKilledEvent(EnemyHealth enemyHealth)
    {
        playerScore += enemyHealth.ScoreValue;
        UpdateScore();
    }

    private void OnEnemyBossKilledEvent(EnemyBossHealth enemyBossHealth)
    {
        playerScore += enemyBossHealth.ScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = playerScore.ToString("0");
    }

}
