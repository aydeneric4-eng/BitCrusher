using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;

public static class GameManager
{
    private static int score = 0;
    public static PlayerManager playerIntance;
    public static AStarManager pathfindingInstance;

    public static event Action<int> scoreUpdated;

    public static void ResetScore()
    {
        score = 0;
        scoreUpdated.Invoke(score);
    }
    public static void AddScore(int value)
    {
        score += value;
        scoreUpdated.Invoke(score);
    }
}