using UnityEngine;
using System.Collections;

public class BossParameters : Parameters
{
    [SerializeField] int expReward;
    public override void Die(){
        if(GetComponent<BaseBoss>().isDefeated) return;
        EventManager.BossDied.Invoke(this);
        EventManager.PlaySoundAtLocation.Invoke(deathSounds[Random.Range(0, deathSounds.Length)], transform.position);
        EventManager.GainExp.Invoke(expReward);
        GetComponent<BaseBoss>().isDefeated = true;
        GetComponent<AnimationPlayer>().PlayAnimation("Death");
        GetComponent<AnimationPlayer>().allowOtherAnimations = false;
        this.enabled = false;
        Destroy(gameObject,1);
        EventManager.Save.Invoke();
    }
}
