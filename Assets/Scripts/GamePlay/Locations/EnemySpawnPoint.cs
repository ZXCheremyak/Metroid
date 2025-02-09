using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] Enemy enemyToSpawn;

    Enemy spawnedEnemy;

    void Start(){
        EventManager.PlayerDied.AddListener(Respawn);
        EventManager.CheckPointUsed.AddListener(Respawn);

        Spawn();
    }

    public void Spawn(){
        if(spawnedEnemy == null && enemyToSpawn != null){
            spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }

    public void Despawn(){
        if(spawnedEnemy != null)
        {
            spawnedEnemy.transform.position = transform.position;
        }
    }

    public void Respawn(CheckPoint checkPoint){
        Despawn();
        Spawn();
    }

    public void Respawn(){
        Despawn();
        Spawn();
    }
}
