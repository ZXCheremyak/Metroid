using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void ExitToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
