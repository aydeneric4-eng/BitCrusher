using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField] List<Image> icons = new List<Image>();
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;

    private int currentLives = 3;

    private void Start()
    {
        GameManager.Instance.playerLivesUpdated += UpdateIcons;
        UpdateIcons(GameManager.Instance.playerLives);
    }

    private void UpdateIcons(int newValue)
    {
        //Debug.Log(newValue);
        currentLives = Mathf.Clamp(newValue, 0, icons.Count);

        if (icons.Count < 1)
            return;

        //Debug.Log("###icons###");
        //Debug.Log(icons);
        //Debug.Log(icons.Count);
        //Debug.Log(onSprite);
        //Debug.Log(offSprite);
        //Debug.Log("###########");
        for (int i = 0; i < icons.Count; i++)
        {
            if (i <= currentLives - 1)
                icons[i].sprite = onSprite;
            else
                icons[i].sprite = offSprite;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.playerLivesUpdated -= UpdateIcons;
    }

}
