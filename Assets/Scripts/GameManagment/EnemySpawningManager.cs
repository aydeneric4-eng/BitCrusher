using UnityEngine;
using System.Collections.Generic;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    private List<EnemySpawnInfo> enemyTypes = new List<EnemySpawnInfo>();
}
