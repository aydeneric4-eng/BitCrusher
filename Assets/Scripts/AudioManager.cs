using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static bool isQuitting = false;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (isQuitting)
                return null;
            if (!instance)
            {
                //instance = new GameObject().AddComponent<AudioManager>();
                //instance.gameObject.name = "AudioManager";
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
        isQuitting = false;
        Application.quitting += onQuitting;
    }
    private static void onQuitting()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        isQuitting = false;
    }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip BGMusic;
    public AudioClip deathSFX;
    public AudioClip jumpSFX;

    private void Start()
    {
        GameManager.Instance.audioManager = this;
    }
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
