using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool playerDetected;

    Rigidbody2D rb;
    [HideInInspector]public PlayerBase player;

    public bool isRanged;

    EnemyParameters parameters;

    [SerializeField] Transform checkGroundPoint;
    [SerializeField] int moveDirection;

    LayerMask groundLayer;

    public bool closeToEdge;

    Animator animator;

    AnimationPlayer animPlayer;

    void Start()
    {
        animPlayer = GetComponent<AnimationPlayer>();
        animator = GetComponent<Animator>();
        parameters = GetComponent<EnemyParameters>();
        parameters.actionsAllowed = true;
        rb = GetComponent<Rigidbody2D>();
        if(Random.Range(0,2) == 0)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 1;
        }
    }

    void Update(){
        if(player == null)
        {
            return;
        }
        if(isRanged && player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(isRanged && player.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        }
    }


    void FixedUpdate()
    {
        if(!parameters.actionsAllowed) 
        {
            return;
        }

        if(isRanged)
        {
            GetComponentInChildren<EnemyAttack>().Attack();
            animPlayer.PlayAnimation("Idle");
            return;
        }
        Move();

    }

    void Move(){
        Flip();

        animPlayer.PlayAnimation("Walk");

        if(playerDetected){

            MoveTowardsPlayer();
        }
        else{
            Patrol();
        }
    }

    void Patrol(){
        if(closeToEdge)
        {
            moveDirection = -moveDirection;
            closeToEdge = false;
        }

        rb.linearVelocity = new Vector2(moveDirection * parameters.moveSpeed, rb.linearVelocity.y);
    }

    void Flip(){
        if(rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if(rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void MoveTowardsPlayer(){
        rb.linearVelocity = new Vector2((player.transform.position - gameObject.transform.position).normalized.x * parameters.moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.TryGetComponent<PlayerParameters>(out PlayerParameters player))
        {
            player.TakeDamage(parameters.damage, parameters.element);
        }
    }

    void OnDisable(){
        rb.linearVelocity = Vector2.zero;
    }
}