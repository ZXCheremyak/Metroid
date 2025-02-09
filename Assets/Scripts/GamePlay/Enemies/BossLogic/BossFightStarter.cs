using UnityEngine;

public class BossFightStarter : MonoBehaviour
{
    [SerializeField] BaseBoss boss;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(boss.isDefeated || boss.bossFightStarted) return;

        if(coll.CompareTag("Player"))
        {
            boss.GetComponent<BossAttack>().enabled = true;
            boss.StartFight();
            EventManager.BossFightStarted.Invoke();
            Debug.Log("Started");
        }
    }
}