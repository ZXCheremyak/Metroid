using UnityEngine;
using System.Collections;

public class EnemyParameters : Parameters
{
    public int expReward;

    [SerializeField]GameObject dieAnimation;

    public override void TakeDamage(float value, Element element){
        base.TakeDamage(value, element);
        EventManager.enemyDamaged.Invoke();
    }

    public override void Die(){
        GameObject animationInstance = Instantiate(dieAnimation, transform.position, Quaternion.identity);
        Destroy(animationInstance, 1f);
        EventManager.EnemyDied.Invoke(this);
        EventManager.GainExp.Invoke(expReward);
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}
