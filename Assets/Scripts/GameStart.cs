using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        EventManager.Load.Invoke();
    }

    void OnDestroy(){
        EventManager.Save.Invoke();
    }
}
