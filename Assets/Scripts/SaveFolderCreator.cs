using UnityEngine;
using System.IO;

public class SaveFolderCreator : MonoBehaviour
{
    void Start()
    {
        if(!Directory.Exists(Directory.GetCurrentDirectory() + "/saves")) Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/saves");
    }
}
