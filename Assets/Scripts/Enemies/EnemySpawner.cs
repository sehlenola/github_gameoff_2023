using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnRate = 5f; // How often to spawn enemies
    public float minDistanceFromPlayer = 10f; // Minimum distance from the player
    public Transform playerTransform; // Reference to the player's transform
    public List<Transform> spawnPoints; // Manually assigned list of spawn points

    private float currentSpawnTime;

    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= spawnRate)
        {
            SpawnEnemy();
            currentSpawnTime = 0f;
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = ChooseSpawnPoint();
        if (spawnPoint != null)
        {
            GameObject go = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);
        }
    }

    Transform ChooseSpawnPoint()
    {
        List<Transform> validSpawnPoints = new List<Transform>();

        foreach (var point in spawnPoints)
        {
            if (Vector3.Distance(point.position, playerTransform.position) >= minDistanceFromPlayer)
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
}