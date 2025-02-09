using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject deletionScreen;

    public void StartGame(){
        SceneManager.LoadScene("GamePlay");
    }

    public void OpenSettings(){
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings(){
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void DeleteSaves(){
        deletionScreen.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ConfirmDeletion(){
        deletionScreen.SetActive(false);
        mainMenu.SetActive(true);
        Serializer.Delete();
    }

    public void RejectDeletion(){
        deletionScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
}
