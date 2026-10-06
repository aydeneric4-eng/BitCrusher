using UnityEngine;

public class InterludeManager : MonoBehaviour
{
    [SerializeField] SectorCount sc;
    [SerializeField] float interludeDuration = 5f;
    private float interludeStart;

    private void Start()
    {
        if (sc)
            sc.SetCount(GameManager.Instance.currentLevel);
    }

}
