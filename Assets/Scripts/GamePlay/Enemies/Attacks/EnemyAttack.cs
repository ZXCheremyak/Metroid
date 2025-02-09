using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    EnemyParameters parameters;
    [Header("Enemy settings")]
    public bool canAttack = true;
    public bool playerInAttackRange;

    [Header("Attack settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform attackPoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] int attacksNumber;
    [SerializeField] float attackRange;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackDelayTime;

    protected GameObject player;

    IEnumerator attackCoroutine;

    Animator animator;

    AnimationPlayer animPlayer;

    protected virtual void Start(){
        GetComponent<CircleCollider2D>().radius = attackRange;
    }

    public void Attack(){
        if(playerInAttackRange && canAttack && player != null)
        {
            Debug.Log("enemy Attack");
            StartCoroutine("AttackProcess");
        }
    }

    IEnumerator AttackProcess()
    {
        canAttack = false;
        GetComponentInParent<Parameters>().actionsAllowed = false;

        GetComponentInParent<AnimationPlayer>().PlayAnimation("AttackStart");
        GetComponentInParent<AnimationPlayer>().allowOtherAnimations = false;
        
        yield return new WaitForSeconds(attackDelayTime);

        int i = 0;

        for(i = 0; i < attacksNumber; i++)
        {
            if(player == null)
            {
                break;
            }
            GetComponentInParent<AnimationPlayer>().allowOtherAnimations = true;
            GetComponentInParent<AnimationPlayer>().PlayAnimation("AttackEnd");
            PerformAttack();
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        GetComponentInParent<Parameters>().actionsAllowed = true;
        GetComponentInParent<AnimationPlayer>().PlayAnimation("Idle");

        yield return new WaitForSeconds(attackCooldown * i / attacksNumber);

        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            playerInAttackRange = true;
            player = other.gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            playerInAttackRange = false;
            player = null;
        }
    }

    void PerformAttack(){
        GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileInstance.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - attackPoint.position).normalized * projectileSpeed;
        projectileInstance.GetComponent<Projectile>().element = GetComponentInParent<Parameters>().element;
        projectileInstance.GetComponent<Projectile>().damage = GetComponentInParent<Parameters>().damage;
    }
}