using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnInfo", menuName = "Scriptable Objects/EnemySpawnInfo")]
public class EnemySpawnInfo : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyCellSize = 1;
    
}
