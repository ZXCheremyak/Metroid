using UnityEngine;

public abstract class BaseBoss : MonoBehaviour
{
    public bool isDefeated;
    [SerializeField] string bossName;

    public bool bossFightStarted;

    private void Start()
    {
        EventManager.PlayerDied.AddListener(DeactivateBoss);
        EventManager.Save.AddListener(Save);
        EventManager.Load.AddListener(Load);

        Load();
        
        if(isDefeated)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void StartFight(){
        bossFightStarted = true;
    }

    private void Save()
    {
        BossContainer bossContainer = new BossContainer();
        bossContainer.isDefeated = isDefeated;
        Serializer.Save(bossName, bossContainer);
    }

    private void Load(){
        BossContainer bossContainer = Serializer.Load<BossContainer>(bossName);
        if(bossContainer == null) return;

        isDefeated = bossContainer.isDefeated;
    }

    void DeactivateBoss(){
        bossFightStarted = false;
        GetComponent<Parameters>().RestoreHp();
    }

    [System.Serializable]
    public class BossContainer{
        internal bool isDefeated;
    }
}