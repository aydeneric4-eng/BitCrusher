using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
