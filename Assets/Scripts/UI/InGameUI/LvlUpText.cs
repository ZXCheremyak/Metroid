using UnityEngine;

public class LvlUpText : MonoBehaviour
{
    [SerializeField] GameObject lvlUpText1;
    [SerializeField] Transform spawnPos;
    void Start()
    {
        
        EventManager.lvlUp.AddListener(SpawnLvlupText);
    }

    void SpawnLvlupText(){
        GameObject lvlupText1 = Instantiate(lvlUpText1, spawnPos.position, Quaternion.identity);
    }
}
