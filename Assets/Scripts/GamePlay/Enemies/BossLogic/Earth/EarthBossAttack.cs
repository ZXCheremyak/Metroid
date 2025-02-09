using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthBossAttack : BossAttack
{
    [SerializeField] int enemiesNumber;
    int enemiesSpawned;

    [SerializeField] Enemy skeletonPrefab;

    [SerializeField] GameObject skeletonSpawnVisual;
    
    [SerializeField] Transform leftArenaPos, rightArenaPos;

    protected override void Start(){
        EventManager.EnemyDied.AddListener(RemoveEnemyFromList);

        base.Start();
    }

    public override void PerformAttack(int value)
    {
        if(enemiesNumber <= 0) return;
        Debug.Log("BOSS ATTACKS");
        if(!GetComponent<BaseBoss>().bossFightStarted) return;
        StartCoroutine("SpawnSkeleton");
    }

    IEnumerator SpawnSkeleton(){
        if(enemiesSpawned < 5)
        {
            GameObject spawnVisualPrefab = Instantiate(skeletonSpawnVisual, new Vector2(Random.Range(leftArenaPos.position.x, rightArenaPos.position.x), leftArenaPos.position.y / 2 + rightArenaPos.position.y / 2), Quaternion.identity);
            yield return new WaitForSeconds(1);
            GameObject skeleton = Instantiate(skeletonPrefab.gameObject, new Vector2(spawnVisualPrefab.transform.position.x, spawnVisualPrefab.transform.position.y + 1), Quaternion.identity);
            Destroy(spawnVisualPrefab);
            skeleton.GetComponent<Enemy>().enabled = true;
            enemiesSpawned++;
        }
        yield return new WaitForSeconds(1);
        PerformAttack(0);
    }

    void RemoveEnemyFromList(EnemyParameters enemy)
    {
        enemiesNumber--;
        enemiesSpawned--;
        if(enemiesNumber == 0)
        {
            GetComponent<BossParameters>().Die();
            GetComponent<BaseBoss>().isDefeated = true;
            EventManager.Save.Invoke();
            this.enabled = false;
        }
    }
}
