using UnityEngine;
using System.Collections;

public class Parameters : MonoBehaviour
{
    public float attackDamage;

    public Element element;

    public float maxHp;

    public float hp;

    public float moveSpeed;

    public float moveSpeedModifier;

    float speedRecoverTimer;

    void Update(){
        if(speedRecoverTimer > 0){
            speedRecoverTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float value, Element attackingElement){
        
        hp -= value;
        if(hp <= 0)
        {
            Die();
        }
    }

    public void RecoverHealth(float value){
        hp += value;
        if(hp > maxHp){
            hp = maxHp;
        }
    }

    public void ChangeSpeed(float value, float time){
        moveSpeedModifier = value;
        speedRecoverTimer = time;

    }



    void Die(){
        Destroy(gameObject);
    }

    Result CompareElements(Element element1, Element element2){
        return Result.Stronger;
        return Result.Weaker;
        return Result.Equal;
    }
}

public enum ElementName
{
    None,
    Fire,
    Water,
    Air,
    Earth
}

public enum Result
{
    Stronger,
    Equal,
    Weaker
}