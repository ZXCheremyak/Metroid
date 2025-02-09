using UnityEngine;

public class FireBossMovement : BossMovement
{
    public override void TeleportToAnotherPosition()
    {
        transform.position = positions[Random.Range(0, positions.Length)].position;
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        GetComponent<FireBossAttack>().PerformAttack(Random.Range(0,3));
    }
}
