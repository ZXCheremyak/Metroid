using UnityEngine;

public class BossDoor : MonoBehaviour
{
    void Start(){
        EventManager.BossFightStarted.AddListener(Close);
        EventManager.BossDied.AddListener(Open);
        EventManager.PlayerDied.AddListener(Open);
    }

    void Close(){
        transform.position = new Vector2(transform.position.x, transform.position.y - 3);
    }

    void Open(Parameters parameters){
        transform.position = new Vector2(transform.position.x, transform.position.y + 3);
    }

    void Open(){
        transform.position = new Vector2(transform.position.x, transform.position.y + 3);
    }
}
