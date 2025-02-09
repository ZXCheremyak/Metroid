using UnityEngine;

public class WaterBoss: BaseBoss
{
    public override void StartFight()
    {
        EventManager.BossDied.Invoke(GetComponent<BossParameters>());
        isDefeated = true;
        EventManager.Save.Invoke();
    }
}
