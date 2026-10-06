using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] List<LevelSpawnSettings> levelSettings = new List<LevelSpawnSettings>();
    [SerializeField] List<GameObject> activeEnemies = new List<GameObject>();

    [SerializeField] float minTimeBetweenSpawnWaves = 5f;
    [SerializeField] float maxTimeBetweenSpawnWaves = 20f;
    private float timeSinceLastWave = -999f;
    private float timeSinceLastEnemy = -999f;

    [SerializeField] float minTimeBetweenLevelSwap = 5f;
    private float timerstrt;

    private Dictionary<EnemySpawnInfo, int> enemiesToSpawn = new Dictionary<EnemySpawnInfo, int>();
    //private int maxEnemiesPerWave;

    private LevelSpawnSettings currentLevelSettings;

    private bool finalWave = false;


    private void AddEnemy(GameObject enemy) => activeEnemies.Add(enemy);
    private void Start()
    {
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.spawnedEnemy += AddEnemy;
        }
        SetLevelSettings();
        SetUpWaves();
        timeSinceLastWave = Time.time;
        timeSinceLastEnemy = Time.time;
    }

    private void FixedUpdate()
    {

        float c = activeEnemies.Count;
        activeEnemies.RemoveAll(x => !x);
        if (c != activeEnemies.Count)
            timeSinceLastEnemy = Time.time;

        if (activeEnemies.Count < 1 && enemiesToSpawn.Count > 0 && !CustomUtilities.HasTimeElapsed(timeSinceLastEnemy, minTimeBetweenSpawnWaves))
        {
            GameManager.Instance.TriggerEnemyWaveEvent(true);
        }
        else
        {
            GameManager.Instance.TriggerEnemyWaveEvent(false);
        }
        if (activeEnemies.Count < 1 && CustomUtilities.HasTimeElapsed(timeSinceLastEnemy, minTimeBetweenSpawnWaves)) //|| CustomUtilities.HasTimeElapsed(timeSinceLastWave, maxTimeBetweenSpawnWaves))
        {
            if (enemiesToSpawn.Count < 1)
            {
                GameManager.Instance.GoToInterlude();
            }
            else
            {
                //Debug.Log("spawn wabe new");
                SpawnWave();
            }
        }
    }

    private void SetLevelSettings()
    {
        List<LevelSpawnSettings> sortedSettings = new List<LevelSpawnSettings>(levelSettings);
        sortedSettings.OrderBy(o => o.levelRequierment);
        currentLevelSettings = sortedSettings[0];
        sortedSettings.RemoveAt(0);

        while (true)
        {
            if (sortedSettings.Count < 1)
                break;
            if (sortedSettings[0].levelRequierment <= GameManager.Instance.currentLevel)
            {
                currentLevelSettings = sortedSettings[0];
                sortedSettings.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
        //Debug.Log(currentLevelSettings.levelRequierment);
        //Debug.Log("Finished setting up lv settings");
    }
    private void SetUpWaves()
    {
        int enemiesOfTypeToAdd;
        for (int i = 0; i < currentLevelSettings.enemyTypes.Count; i++)
        {
            enemiesOfTypeToAdd = Random.Range(currentLevelSettings.minNumberToSpawn[i], currentLevelSettings.maxNumberToSpawn[i] + 1);
            //Debug.Log(enemiesOfTypeToAdd);
            //Debug.Log(currentLevelSettings.minNumberToSpawn[i]);
            //Debug.Log(currentLevelSettings.maxNumberToSpawn[i]);
            enemiesToSpawn.Add(currentLevelSettings.enemyTypes[i], enemiesOfTypeToAdd);
            //Debug.Log("ENEMY ADD LOOP");
        }
        
        //Debug.Log("Finished setting up waves");
    }
    private void SpawnWave()
    {
        List<EnemySpawner> spawnersAvailable = new List<EnemySpawner>(enemySpawners);
        //int enemiesQueued = 0;
        EnemySpawner selectedSpawner;
        EnemySpawnInfo selectedEnemy;
        //Debug.Log("start quueing enemy spawns");
        
        for (int i = 0; spawnersAvailable.Count > 0 && enemiesToSpawn.Count > 0 && i < currentLevelSettings.maxEnemiesPerWave; i++)
        {
            selectedSpawner = spawnersAvailable[Random.Range(0, spawnersAvailable.Count)];
            selectedEnemy = enemiesToSpawn.Keys.ToList()[Random.Range(0, enemiesToSpawn.Keys.Count )];

            //Debug.Log("Quueing enemy spawn w/ spawner");
            //Debug.Log(enemiesToSpawn[selectedEnemy]);
            selectedSpawner.QueueSpawn(selectedEnemy, 1);
            enemiesToSpawn[selectedEnemy] -= 1;
            if (enemiesToSpawn[selectedEnemy] <= 0)
                enemiesToSpawn.Remove(selectedEnemy);
            if (selectedSpawner.IsSpawnQueueFull())
                spawnersAvailable.Remove(selectedSpawner);
        }

        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.SpawnEnemies();
        }

        timeSinceLastWave = Time.time;
        if (enemiesToSpawn.Count < 1)
        {
            finalWave = true;
        }
        //Debug.Log("Finished spawning wave");
    }
}
