using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider sfxSlider, musicSlider;
    public TextMeshProUGUI sfxText, musicText;
 
    void Start(){
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SetSFXVolume(){
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        EventManager.changeSfxVolume.Invoke(sfxSlider.value);
        sfxText.text = "Sound effects volume: " + (sfxSlider.value*100).ToString("#.");
    }

    public void SetMusicVolume(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        EventManager.changeSfxVolume.Invoke(musicSlider.value);
        musicText.text = "Music volume: " + (musicSlider.value*100).ToString("#.");
    }
}