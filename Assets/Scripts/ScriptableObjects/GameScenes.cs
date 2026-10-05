using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scenes", menuName = "Scriptable Objects/Scenes")]
public class GameScenes : ScriptableObject
{
    public SceneAsset mainMenu;
    public SceneAsset gameOver;

    public List<SceneAsset> layouts = new List<SceneAsset>();
}
