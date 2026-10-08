using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scenes", menuName = "Scriptable Objects/Scenes")]
public class GameScenes : ScriptableObject
{
    [Scene]
    public string mainMenu;
    [Scene]
    public string gameOver;
    [Scene]
    public string interlude;
    [Scene]
    public List<string> layouts = new List<string>();
    [Scene]
    public List<string> levelSpecificLayouts = new List<string>();
}
