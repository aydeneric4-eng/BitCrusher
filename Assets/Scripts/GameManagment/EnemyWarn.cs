using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class EnemyWarn : MonoBehaviour
{
    private TMP_Text selfTMP;

    private void Awake()
    {
        //Debug.LogError("THIS NOT IMPLIMENTED VRO");
        selfTMP = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        GameManager.Instance.enemyWaveEvent += OnEnemyWaveEvent;
        //Debug.Log("Subbed?");
    }

    private void OnEnemyWaveEvent(bool v)
    {
        //Debug.Log(v);
        selfTMP.enabled = v;
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.enemyWaveEvent -= OnEnemyWaveEvent;
    }
}
