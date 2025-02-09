using UnityEngine;
using System.Collections;

public class PlayerParameters : Parameters
{
    bool canTakeDamage = true;
    [SerializeField] LayerMask enemylayer;
    [SerializeField] protected float invulTime;
    public override void TakeDamage(float value, Element element){
        if(!canTakeDamage) return;

        base.TakeDamage(value, element);

        StartCoroutine("InvulTimer");

        EventManager.ChangePlayerHealth.Invoke(hp, maxHp);
    }

    public override void RecoverHealth(float value){
        base.RecoverHealth(value);
        EventManager.ChangePlayerHealth.Invoke(hp, maxHp);
    }

    public override void RestoreHp(CheckPoint c){
        base.RestoreHp(c);
        EventManager.ChangePlayerHealth.Invoke(hp, maxHp);
    }

    public override void Die(){
        EventManager.PlayerDied.Invoke();
        RestoreHp(null);
        EventManager.PlaySound.Invoke(deathSounds[Random.Range(0, deathSounds.Length)]);
    }

    IEnumerator InvulTimer(){
        canTakeDamage = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for(int i = 0; i < 12; i++)
        {
            sr.color = new Color(1, 1, 1, 1 - 0.5f * Mathf.Cos(Mathf.Deg2Rad * 180f * i));
            yield return new WaitForSeconds(invulTime / 10f);
        }
        canTakeDamage = true;
        yield return null;
        sr.color = new Color(1,1,1,1);
    }
}
