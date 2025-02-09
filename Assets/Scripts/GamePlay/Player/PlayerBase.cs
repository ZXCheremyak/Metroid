using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
    [Header("Experience")]
    public int Level;
    public int expToNextLevel;
    public int currentExp;
    
    [SerializeField] Transform firstLocationSpawn;

    Vector2 lastUsedCheckpointPos;
    
    IEnumerator smoothExpGain;

    [SerializeField] AudioClip lvlupClip;

    void Start(){
        EventManager.GainExp.AddListener(CallGainExpCoroutine);
        EventManager.CheckPointUsed.AddListener(SaveCheckPointPosition);
        EventManager.Save.AddListener(Save);
        EventManager.Load.AddListener(Load);

        EventManager.PlayerDied.AddListener(TeleportToLastCheckpoint);
        EventManager.PlayerDied.AddListener(NullifyExp);

        Load();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.TryGetComponent<IInteractible>(out IInteractible interactibleObject))
        {
            GetComponent<Movement>().closestInteractObject = interactibleObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.TryGetComponent<IInteractible>(out IInteractible interactibleObject))
        {
            GetComponent<Movement>().closestInteractObject = null;
        }
    }

    void SaveCheckPointPosition(CheckPoint checkPoint){
        lastUsedCheckpointPos = checkPoint.transform.position;
        Debug.Log(lastUsedCheckpointPos);
    }

    void TeleportToLastCheckpoint(){
        if(lastUsedCheckpointPos == Vector2.zero)
        {
            transform.position = firstLocationSpawn.position;
            return;
        }
        transform.position = lastUsedCheckpointPos;
        EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
    }

    void CallGainExpCoroutine(int exp){
        smoothExpGain = GainExp(exp);
        StartCoroutine(smoothExpGain);
    }

    IEnumerator GainExp(float expReward){
        for(int i = 0; i < expReward; i++)
        {
            currentExp += 1;
            if(currentExp >= expToNextLevel){
                currentExp -= expToNextLevel;
                expToNextLevel += (int)((float)expToNextLevel * 1.2f);
                
                EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
                LevelUp();
                
            }
            
            EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    void NullifyExp(){
        currentExp = 0;
        
        EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
    }

    void LevelUp(){
        GetComponent<Parameters>().damageMultiplier += 0.2f;
        GetComponent<Parameters>().maxHp *= 1.05f;
        EventManager.PlaySound.Invoke(lvlupClip);
        EventManager.lvlUp.Invoke();
        
        EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
        //GainExp();
    }

    void Save(){
        PlayerContainer playerContainer = new PlayerContainer();

        playerContainer.damageMultiplier = GetComponent<Parameters>().damageMultiplier;
        playerContainer.maxHp = GetComponent<Parameters>().maxHp;
        playerContainer.currentExp = currentExp;
        playerContainer.expToNextLevel = expToNextLevel;
        playerContainer.Level = Level;
        playerContainer.lastUsedCheckpointPosX = lastUsedCheckpointPos.x;
        playerContainer.lastUsedCheckpointPosY = lastUsedCheckpointPos.y;
        Serializer.Save<PlayerContainer>("PlayerContainer", playerContainer);
    }

    void Load()
    {
        if(!Serializer.CheckFileExistance("PlayerContainer"))
        {
            currentExp = 0;
            expToNextLevel = 100;
            Level = 1;
            GetComponent<Parameters>().maxHp = 100;
            GetComponent<Parameters>().hp = 100;
            TeleportToLastCheckpoint();
            EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);
            return;
        }

        PlayerContainer playerContainer = Serializer.Load<PlayerContainer>("PlayerContainer");

        GetComponent<Parameters>().damageMultiplier =  playerContainer.damageMultiplier;
        GetComponent<Parameters>().maxHp = playerContainer.maxHp;
        currentExp = playerContainer.currentExp;
        expToNextLevel = playerContainer.expToNextLevel;
        Level = playerContainer.Level;
        lastUsedCheckpointPos.x = playerContainer.lastUsedCheckpointPosX;
        lastUsedCheckpointPos.y = playerContainer.lastUsedCheckpointPosY;

        EventManager.ChangeExp.Invoke(currentExp, expToNextLevel);

        TeleportToLastCheckpoint();
    }

    [System.Serializable]
    class PlayerContainer{
        internal float damageMultiplier, maxHp;
        internal int currentExp, expToNextLevel, Level;
        internal float lastUsedCheckpointPosX, lastUsedCheckpointPosY;
    }
}