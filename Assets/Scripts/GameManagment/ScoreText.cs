using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] string prefixText = "Score: ";

    private TMP_Text selfTMP;

    private void Awake()
    {
        selfTMP = GetComponent<TMP_Text>();
        GameManager.Instance.scoreUpdated += OnScoreUpdated;

        selfTMP.text = prefixText + "0";
    }
    private void OnScoreUpdated(int newScore)
    {
        selfTMP.text = prefixText + newScore.ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.scoreUpdated -= OnScoreUpdated;
    }

}
