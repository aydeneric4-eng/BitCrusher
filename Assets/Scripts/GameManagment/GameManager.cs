using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameScenes gameScenes;

    private static bool isQuitting = false;

    private static GameManager instance;
    public static GameManager Instance 
    { 
        get 
        {
            if (isQuitting)
                return null;
            if (!instance)
            {
                instance = new GameObject().AddComponent<GameManager>();
                instance.gameObject.name = "GameManager";
            }
            return instance;
        } 
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        gameScenes = UnityEngine.Resources.Load<GameScenes>("GameScenes"); // SCREW U UNITYYYYYYYYYYYYY
        DontDestroyOnLoad(gameObject);
        isQuitting = false;
        Application.quitting += onQuitting;
    }
    private static void onQuitting() 
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        isQuitting = false;
    }

    public int currentLevel = 1;
    public int score = 0;
    public int playerLives { get; private set; } = 3;
    private const int MaxPlayerLives = 3;
    public PlayerManager playerIntance;
    public AStarManager pathfindingInstance;
    public AudioManager audioManager;

    public event Action<bool> enemyWaveEvent;
    public void TriggerEnemyWaveEvent(bool v)
    {
        //Debug.Log("E WAVE EVENT!");
        //Debug.Log(v);
        enemyWaveEvent?.Invoke(v);
    }

    public event Action<int> scoreUpdated;
    public event Action<int> playerLivesUpdated;

    public void ResetScore()
    {
        score = 0;
        scoreUpdated?.Invoke(score);
    }
    public void AddScore(int value)
    {
        score += value;
        scoreUpdated?.Invoke(score);
    }

    public void ReducePlayerLives(int amount = -1)
    {
        playerLives = Mathf.Clamp(playerLives + amount, 0, MaxPlayerLives);
        playerLivesUpdated?.Invoke(playerLives);
    }

    public void GotoMainMenu()
    {
        if (!gameScenes)
        {
            Debug.LogError("NO GAME SCENES");
            return;
        }
        SceneManager.LoadScene(gameScenes.mainMenu.name);
    }
    public void GotoLossScreen()
    {
        if (!gameScenes)
        {
            Debug.LogError("NO GAME SCENES");
            return;
        }
        SceneManager.LoadScene(gameScenes.gameOver.name);
    }

    private void LoadDaNextLevel()
    {
        SceneAsset sceneToLoad = gameScenes.layouts[UnityEngine.Random.Range(0, gameScenes.layouts.Count)];
        //Debug.Log(currentLevel - 1);
        if (gameScenes.levelSpecificLayouts.Count > currentLevel)
        {
            if (gameScenes.levelSpecificLayouts[currentLevel - 1])
            {
                sceneToLoad = gameScenes.levelSpecificLayouts[currentLevel - 1];
            }
        }
        //Debug.Log(currentLevel);
        SceneManager.LoadScene(sceneToLoad.name);
    }

    public void StartGame()
    {
        if (!gameScenes)
        {
            Debug.LogError("NO GAME SCENES");
            return;
        }
        score = 0;
        currentLevel = 1;
        playerLives = MaxPlayerLives;
        LoadDaNextLevel();
    }
    public void GotoNextLevel()
    {
        if (!gameScenes)
        {
            Debug.LogError("NO GAME SCENES");
            return;
        }
        currentLevel++;
        LoadDaNextLevel();
    }

    public void GoToInterlude()
    {
        if (!gameScenes)
        {
            Debug.LogError("NO GAME SCENES");
            return;
        }
        SceneManager.LoadScene(gameScenes.interlude.name);
    }
}