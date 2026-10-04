using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] List<LevelSpawnSettings> levelSettings = new List<LevelSpawnSettings>();
    [SerializeField] List<GameObject> activeEnemies = new List<GameObject>();

    [SerializeField] float minTimeBetweenSpawnWaves = 3f;
    [SerializeField] float maxTimeBetweenSpawnWaves = 8f;

    private Dictionary<EnemySpawnInfo, int> enemiesToSpawn;
    private int maxEnemiesPerWave;

    private LevelSpawnSettings currentLevelSettings;

    private void AddEnemy(GameObject enemy) => activeEnemies.Add(enemy);
    private void Start()
    {
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.spawnedEnemy += AddEnemy;
        }
        SetLevelSettings();
    }
    private void SetLevelSettings()
    {
        List<LevelSpawnSettings> sortedSettings = new List<LevelSpawnSettings>(levelSettings);
        sortedSettings.OrderBy(o => o.levelRequierment);
        currentLevelSettings = sortedSettings[0];
        sortedSettings.RemoveAt(0);
        bool foundSettings = false;
        while (!foundSettings)
        {
            if (sortedSettings[0].levelRequierment <= GameManager.Instance.currentLevel)
            {
                currentLevelSettings = sortedSettings[0];
                sortedSettings.RemoveAt(0);
            }
            else
            {
                foundSettings = true;
                break;
            }
        }
    }
    private void SetUpWaves()
    {
        maxEnemiesPerWave = 0;
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            maxEnemiesPerWave += enemySpawner.GetMaxSpawnCount();
        }
        for (int i = 0; i < currentLevelSettings.enemyTypes.Count; i++)
        {
            int enemiesOfTypeToAdd = UnityEngine.Random.Range(currentLevelSettings.minNumberToSpawn[i], currentLevelSettings.maxNumberToSpawn[i]);
            enemiesToSpawn.Add(currentLevelSettings.enemyTypes[0], enemiesOfTypeToAdd);
        }
    }
    private void SpawnWave()
    {
        bool isWaveFull = false;
        for (int i = 0; !isWaveFull && enemiesToSpawn.Count > 0; i++)
        {

        }
    }
}
