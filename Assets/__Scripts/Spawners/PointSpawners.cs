using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class PointSpawners : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;

    public delegate void EnemySpawned();

    public static event EnemySpawned EnemySpawnedEvent;
    int randEnemy;

    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        Invoke("EnableSpawning", 5);
    }

    private void SpawnEnemyWaves()
    {
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    private void SpawnOneEnemy()
    {
        randEnemy = Random.Range(0, 2);

        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab[randEnemy], enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
        PublishEnemySpawnedEvent();
    }

    public void PublishEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke();
    }

    public void EnableSpawning()
    {
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }

}
