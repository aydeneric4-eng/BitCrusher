using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelSpawnSettings", menuName = "Scriptable Objects/LevelSpawnSettings")]
public class LevelSpawnSettings : ScriptableObject
{
    public int levelRequierment = 1;
    public List<EnemySpawnInfo> enemyTypes = new List<EnemySpawnInfo>();
    public List<int> minNumberToSpawn = new List<int>();
    public List<int> maxNumberToSpawn = new List<int>();
    public List<int> chanceOfSpawn = new List<int>();
    public int minTotalEnemies = 1;
    public int maxTotalEnemies = 1;

}
