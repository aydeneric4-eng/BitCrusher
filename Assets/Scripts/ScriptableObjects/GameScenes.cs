using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scenes", menuName = "Scriptable Objects/Scenes")]
public class GameScenes : ScriptableObject
{
    public Scene mainMenu;
    public Scene gameOver;

    public List<Scene> layouts = new List<Scene>();
}
