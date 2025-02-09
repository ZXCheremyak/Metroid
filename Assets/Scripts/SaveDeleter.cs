using UnityEngine;
using System.Collections.Generic;

public class SaveDeleter : MonoBehaviour
{    
    public void DeleteAllSavedData(){
        Serializer.Delete();
        Debug.Log("Successful");
    }
}