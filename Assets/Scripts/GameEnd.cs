using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour, IInteractible
{
    public void Interact(PlayerBase player){
        SceneManager.LoadScene("GameEnd");
    }
}
