using UnityEngine;

public class FireBoss : BaseBoss
{
    public override void StartFight(){
        bossFightStarted = true;
        GetComponent<FireBossMovement>().TeleportToAnotherPosition();
    }
}
