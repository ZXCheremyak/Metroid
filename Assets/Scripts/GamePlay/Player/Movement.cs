using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerParameters parameters;


    [Header("Move")]
    [SerializeField]bool canMove;

    Vector2 moveVector;


    [Header("Dodge")]
    [SerializeField] AudioClip dodgeSound;

    [SerializeField]bool canDodge;

    [SerializeField] float dodgeSpeed;

    [SerializeField] float dodgeCD;

    [SerializeField]float dodgeTime;


    [Header("Jump")]
    public bool canJump;
    [SerializeField] AudioClip jumpSound;

    [SerializeField]float  maxFallingSpeed;

    [SerializeField]float jumpForce;

    Animator animator;

    public bool allowOtherAnimations;

    AnimationPlayer animPlayer;

    float startGravityScale;
    [HideInInspector] public IInteractible closestInteractObject;


    void Start()
    {   
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        parameters = GetComponent<PlayerParameters>();
        animPlayer = GetComponent<AnimationPlayer>();
        
        startGravityScale = rb.gravityScale;
    }

    void Update()
    {
        if(!parameters.actionsAllowed) return;

        Move();
        Jump();
        Dodge();
        Flip();
        Interact();
    }

    void Move()
    {
        if(!canMove) 
        {
            return;
        }

        moveVector.x = Input.GetAxisRaw("Horizontal");

        if(parameters.isGrounded)
        {
            if(moveVector != Vector2.zero)
            {
                animPlayer.PlayAnimation("Run");
            }
            else{
                animPlayer.PlayAnimation("Idle");
            }
        }
        
        rb.linearVelocity = new Vector2(moveVector.x * parameters.moveSpeed, rb.linearVelocity.y);        
        if(rb.linearVelocity.y < -maxFallingSpeed){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallingSpeed);
        }
    }

    void Flip(){
        if(rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-0.7f,0.7f,0.7f);
        }
        else if(rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
    }

    void Jump()
    {
        if(!parameters.isGrounded)
        {   
            
            if(rb.linearVelocity.y < 0)
            {
                animPlayer.PlayAnimation("Fall");
            }
            else if(rb.linearVelocity.y > 0)
            {
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                }
            }
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(new Vector2(0, 1) * jumpForce);
            parameters.isGrounded = false;
            animPlayer.PlayAnimation("Jump");
        }

    }

    void Dodge(){
        if(!canDodge) return;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        StartCoroutine("DodgeCoroutine");
    }

    IEnumerator DodgeCoroutine()
    {
        canMove = false;
        canDodge = false;
        parameters.actionsAllowed = false;

        animPlayer.PlayAnimation("Dodge");

        rb.gravityScale = 0;

        rb.linearVelocity = new Vector2(transform.localScale.x, 0) * dodgeSpeed;
        
        EventManager.PlaySound.Invoke(dodgeSound);

        yield return new WaitForSeconds(dodgeTime);
        
        canMove = true;

        parameters.actionsAllowed = true;
        
        rb.gravityScale = startGravityScale;

        rb.linearVelocity = new Vector2(0, 0);

        yield return new WaitForSeconds(dodgeCD);

        canDodge = true;
    }
    
    void Interact(){
        if(Input.GetKeyDown(KeyCode.E) && closestInteractObject != null){
            closestInteractObject.Interact(GetComponent<PlayerBase>());
        }
    }

    public void PlayAnimation(string animation){
        if(!allowOtherAnimations) return;
        animator.Play(animation);
    }
}
