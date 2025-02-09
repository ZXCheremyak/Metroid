using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioSource aSource;
    AudioClip currentMusicPlaying;

    [SerializeField] AudioClip[] music;

    [SerializeField] AudioClip bossFightMusic;

    bool bossFight;

    void Awake(){
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        
        EventManager.changeMusicVolume.AddListener(ChangeVolume);
    }

    void Start()
    {
        aSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        EventManager.changeMusicVolume.AddListener(ChangeVolume);
    }

    void Update()
    {
        aSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        if(!aSource.isPlaying && !bossFight)
        {
            aSource.Pause();
            PlayMusic(GetRandomClip(music));
        }
        else if(!aSource.isPlaying && bossFight)
        {
            aSource.Pause();
            PlayMusic(bossFightMusic);
        }
        if(aSource.clip != bossFightMusic && bossFight)
        {
            aSource.Pause();
            PlayMusic(bossFightMusic);
        }
    }

    void PlayMusic(AudioClip clip){
        AudioClip nextClip = GetRandomClip(music);

        aSource.clip = nextClip;
        aSource.Play();
        currentMusicPlaying = nextClip;
    }

    void ChangeVolume(float value){
        Debug.Log(value);
        aSource.volume = value;
    }

    AudioClip GetRandomClip(AudioClip[] clips){
        return clips[Random.Range(0, clips.Length)];
    }
}
