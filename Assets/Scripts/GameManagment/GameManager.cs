using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    { 
        get 
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<GameManager>();
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
        DontDestroyOnLoad(gameObject);
    }


    private int score = 0;
    public int playerLives { get; private set; } = 3;
    private const int MaxPlayerLives = 3;
    public PlayerManager playerIntance;
    public AStarManager pathfindingInstance;

    public event Action<int> scoreUpdated;
    public event Action<int> playerLivesUpdated;

    public void ResetScore()
    {
        score = 0;
        scoreUpdated.Invoke(score);
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

}