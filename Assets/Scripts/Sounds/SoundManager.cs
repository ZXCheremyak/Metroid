using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]AudioSource aSource;

    void Awake(){
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start(){
        if(PlayerPrefs.GetFloat("SfxVolume") == 0)
        {
            aSource.volume = 0.5f;
            EventManager.changeSfxVolume.Invoke(0.5f);
        }
        aSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        
        EventManager.PlaySoundAtLocation.AddListener(PlaySoundAtLocation);

        EventManager.PlaySound.AddListener(PlaySound);

        EventManager.changeSfxVolume.AddListener(ChangeVolume);
    }
    
    void Update(){
        aSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    void PlaySound(AudioClip clip){
        aSource.PlayOneShot(clip);
    }

    void PlaySoundAtLocation(AudioClip clip, Vector2 position){
        AudioSource.PlayClipAtPoint(clip, position);
    }

    void ChangeVolume(float value){
        aSource.volume = value;
    }
}
