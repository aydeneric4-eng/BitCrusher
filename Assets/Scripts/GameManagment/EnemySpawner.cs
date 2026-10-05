using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour
{
    private Dictionary<EnemySpawnInfo, int> enemiesToSpawn = new Dictionary<EnemySpawnInfo, int>();
    private BoxCollider2D _boxCollider;
    private List<Vector3> spawnPositions = new List<Vector3>();

    public event Action<GameObject> spawnedEnemy;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        SetupSpawner();
    }

    public bool IsSpawnQueueFull()
    {
        SetupSpawner();
        if (spawnPositions.Count > enemiesToSpawn.Values.Sum())
            return false;
        return true;
    }
    public void SetupSpawner()
    {
        spawnPositions = new List<Vector3>();
        int minX = (int)Mathf.Floor(transform.position.x - _boxCollider.bounds.size.x / 2f);
        int maxX = (int)Mathf.Floor(transform.position.x + _boxCollider.bounds.size.x / 2f);
        int minY = (int)Mathf.Floor(transform.position.y - _boxCollider.bounds.size.y / 2f);
        int maxY = (int)Mathf.Floor(transform.position.y + _boxCollider.bounds.size.y / 2f);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                spawnPositions.Add(new Vector3(x + 0.5f, y + 0.5f, 0.5f));
            }
        }
    }
    public void QueueSpawn(EnemySpawnInfo enemyInfo, int numToSpawn)
    {
        if (enemiesToSpawn.ContainsKey(enemyInfo))
            enemiesToSpawn[enemyInfo] += numToSpawn;
        else
            enemiesToSpawn.Add(enemyInfo, numToSpawn);
    }
    public void SpawnEnemies()
    {
        SetupSpawner();

        foreach (EnemySpawnInfo enemyInfo in enemiesToSpawn.Keys)
        {
            for (int i = 0; i < enemiesToSpawn[enemyInfo] && spawnPositions.Count > 0; i++)
            {
                //Debug.Log("Spawning enemy");
                //Debug.Log(enemiesToSpawn[enemyInfo]);
                Vector3 pos = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count - 1)];
                spawnPositions.Remove(pos);
                spawnedEnemy?.Invoke(Instantiate(enemyInfo.enemyPrefab, pos, Quaternion.identity));
            }
        }
        spawnPositions = new List<Vector3>();
        enemiesToSpawn = new Dictionary<EnemySpawnInfo, int>();
    }

    private bool drawGizmos = true;
    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;
        Gizmos.color = Color.red;
        foreach(Vector3 pos in spawnPositions)
        {
            Gizmos.DrawSphere(pos, 0.5f);
        }
    }

}
