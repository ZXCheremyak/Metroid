using UnityEngine;

public class SaveCall : MonoBehaviour
{
    public void Save(){
        EventManager.Save.Invoke();
    }

    public void Load(){
        EventManager.Load.Invoke();
    }
}
