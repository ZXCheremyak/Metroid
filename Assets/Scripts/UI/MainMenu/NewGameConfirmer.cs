using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewGameConfirmer : MonoBehaviour
{
    [SerializeField] Button agreeButton, disagreeButton;
    void ShowConfirmScreen(){
        
    }

    void ConfirmDeletion(){
        Serializer.Delete();
    }

    void CloseThisMenu(){
        
    }
}
