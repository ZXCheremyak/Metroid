using UnityEngine;
using System.Collections;
using System.IO;

public abstract class Parameters : MonoBehaviour, IHitable
{
    public bool actionsAllowed;

    public bool isGrounded;

    [Header("Offensive")]
    public float damage;

    public float damageMultiplier;


    [Header("Health")]
    public float maxHp;

    public float hp;

    [Header("Other")]
    [SerializeField] protected AudioClip[] deathSounds;
    [SerializeField] protected AudioClip[] takeDamageClips;
    public float moveSpeed;

    public float knockBackPower;

    public Element element;

    void Start(){
        EventManager.CheckPointUsed.AddListener(RestoreHp);
    }

    public virtual void TakeDamage(float value, Element attackingElement)
    {
        if(CompareElements(element, attackingElement) == Result.Weaker){
            hp -= value / 2f;
            Debug.Log(value/2f);
        }
        else if(CompareElements(element, attackingElement) == Result.Stronger)
        {
            hp -= value * 2f;
            Debug.Log(value*2f);
        }
        else{
            hp -= value;
            
            Debug.Log(value);
        }

        StartCoroutine("KnockBackCoroutine");
        StartCoroutine("HurtCoroutine");
        EventManager.PlaySound.Invoke(takeDamageClips[Random.Range(0, takeDamageClips.Length)]);

        if(hp <= 0)
        {
            EventManager.PlaySound.Invoke(deathSounds[Random.Range(0, deathSounds.Length)]);
            StartCoroutine("Die");
        }
    }

    IEnumerator HurtCoroutine(){
        GetComponent<AnimationPlayer>().PlayAnimation("Hit");
        yield return new WaitForSeconds(0.3f);
        GetComponent<AnimationPlayer>().PlayAnimation("Idle");
    }

    public virtual void RecoverHealth(float value)
    {
        hp += value;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public virtual void RestoreHp(CheckPoint c){
        hp = maxHp;
    }

    public void RestoreHp(){
        hp = maxHp;
    }

    public abstract void Die();

    Result CompareElements(Element attackingElement, Element defendingElement)
    {
        if(attackingElement == defendingElement.strongerElement)
        {
            return Result.Weaker;
        }
        if(attackingElement == defendingElement.weakerElement)
        {
            return Result.Stronger;
        }
        return Result.Equal;
    }

    IEnumerator KnockBackCoroutine(){
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localScale.x, 1f) * knockBackPower, ForceMode2D.Impulse);
        actionsAllowed = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        actionsAllowed = true;
    }
}

public enum Result
{
    Stronger,
    Equal,
    Weaker
}