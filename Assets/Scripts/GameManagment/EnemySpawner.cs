using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemySpawnInfo enemyToSpawn;
    [SerializeField] int numberToSpawn;
    private BoxCollider2D _boxCollider;
    private List<Vector3> spawnPositions = new List<Vector3>();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        SpawnEnemies();
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
    public void SpawnEnemies()
    {
        SetupSpawner();

        for (int i = 0; i < numberToSpawn && spawnPositions.Count > 0; i++)
        {
            Vector3 pos = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count - 1)];
            spawnPositions.Remove(pos);
            Instantiate(enemyToSpawn.enemyPrefab, pos, Quaternion.identity);
        }

        spawnPositions = new List<Vector3>();
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
