using UnityEngine;

public class CheckPoint : MonoBehaviour, IInteractible
{

    public void Interact(PlayerBase player){
        EventManager.CheckPointUsed.Invoke(this);
        EventManager.Save.Invoke();
    }
}
