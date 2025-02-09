using UnityEngine;

public class EarthBoss : BaseBoss
{
    public override void StartFight(){
        bossFightStarted = true;
        GetComponent<BossAttack>().PerformAttack(0);
    }
}
