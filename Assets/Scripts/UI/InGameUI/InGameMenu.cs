using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settingsMenu;
    bool menuIsOpen;

    [SerializeField]PlayerParameters player;

    [SerializeField] GameObject inGameUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menuIsOpen)
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
        }
    }

    public void ShowMenu(){
        Time.timeScale = 0;
        player.actionsAllowed = false;
        inGameUI.SetActive(false);
        menu.SetActive(true);
        menuIsOpen = true;
    }

    public void HideMenu(){
        Time.timeScale = 1;
        player.actionsAllowed = true;
        menu.SetActive(false);
        inGameUI.SetActive(true);
        settingsMenu.SetActive(false);
        menuIsOpen = false;
    }

    public void ShowSettingsMenu(){
        menu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void HideSettingsMenu(){
        settingsMenu.SetActive(false);
        ShowMenu();
    }

    public void ExitGame(){
        Time.timeScale = 1;
        EventManager.Save.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}