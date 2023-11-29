using UnityEngine;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnRate = 5f; // How often to spawn enemies
    public float minDistanceFromPlayer = 10f; // Minimum distance from the player
    public Transform playerTransform; // Reference to the player's transform
    public List<Transform> spawnPoints; // Manually assigned list of spawn points

    public List<EnemyWave> enemyWaves;
    public List<EnemyWave> enemyTypesPerLevel;


    private int activeEnemies = 0;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private int minEnemies = 5;
    [SerializeField] private float minEnemiesTimeMultiplier = 2;
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int enemyIndex = 0;
    [SerializeField] private bool isSpawning = true;
    [SerializeField] private float offset = 5f;

    private float currentSpawnTime;

    void OnEnable()
    {
        // Subscribe to events
        StaticEventHandler.OnEnemySpawned += StaticEventHandler_OnEnemySpawned;
        StaticEventHandler.OnEnemyKilled += StaticEventHandler_OnEnemyKilled;
        StaticEventHandler.OnTripComplete += StaticEventHandler_OnTripComplete;
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
    }

    private void StaticEventHandler_OnGameOver(GameOverArgs obj)
    {
        isSpawning = false;
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        isSpawning = false;
    }

    private void StaticEventHandler_OnTripComplete(TripCompleteArgs obj)
    {
        maxEnemies += 5;
        minEnemies += 2;
        if (currentWaveIndex < enemyWaves.Count)
        {
            SpawnWave(enemyWaves[currentWaveIndex]);
        }
        else
        {
            Debug.Log("No more waves to spawn");
        }
        currentWaveIndex++;
    }

    private void StaticEventHandler_OnEnemyKilled(EnemyKilledArgs obj)
    {
        activeEnemies--;
        Debug.Log("Active Enemies: " + activeEnemies);
    }

    private void StaticEventHandler_OnEnemySpawned(EnemySpawnedArgs obj)
    {
        activeEnemies++;
        Debug.Log("Active Enemies: " + activeEnemies);
    }


    void Update()
    {
        if (!isSpawning) return;
        currentSpawnTime += Time.deltaTime;
        if(activeEnemies < minEnemies)
        {
            currentSpawnTime += Time.deltaTime * minEnemiesTimeMultiplier;
        }
        
        if (currentSpawnTime >= spawnRate && activeEnemies <= maxEnemies)
        {

            if (currentWaveIndex < enemyTypesPerLevel.Count)
            {
                EnemyWave currentWave = enemyTypesPerLevel[currentWaveIndex];

                if (enemyIndex < currentWave.enemies.Count)
                {
                    enemyPrefab = currentWave.enemies[enemyIndex];
                    enemyIndex++;
                }
                else
                {
                    enemyIndex = 0; // Reset index to loop back to the first enemy
                    enemyPrefab = currentWave.enemies[enemyIndex];
                    enemyIndex++;
                }
            }
            SpawnEnemy(enemyPrefab);
            currentSpawnTime = 0f;
        }
    }

    void SpawnEnemy(GameObject enemyPrefab, Transform fixedSpawnPoint = null)
    {
        Transform spawnPoint = ChooseSpawnPoint();
        if (fixedSpawnPoint)
        {
            spawnPoint = fixedSpawnPoint;
        }

        if (spawnPoint != null)
        {
            GameObject go = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPoint.position + new Vector3(Random.Range(-offset,offset),0, Random.Range(-offset, offset)), Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);
            StaticEventHandler.CallOnEnemySpawnedEvent();
        }
    }

    Transform ChooseSpawnPoint()
    {
        List<Transform> validSpawnPoints = new List<Transform>();

        foreach (var point in spawnPoints)
        {
            if (playerTransform != null && Vector3.Distance(point.position, playerTransform.position) >= minDistanceFromPlayer)
            {
                validSpawnPoints.Add(point);
            }
        }

        if (validSpawnPoints.Count > 0)
        {
            return validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];
        }

        return null;
    }

    void SpawnWave(EnemyWave wave)
    {
        if (!isSpawning) return;
        Transform fixedSpawnPoint = ChooseSpawnPoint();
        foreach (GameObject entry in wave.enemies)
        {
            SpawnEnemy(entry, fixedSpawnPoint);
        }
    }
}