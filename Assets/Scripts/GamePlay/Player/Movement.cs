using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Parameters parameters;

    [SerializeField]bool canPerformActions;


    [Header("Move")]
    [SerializeField]bool canMove;

    Vector2 moveVector;

    Direction lookDirection;


    [Header("Dodge")]
    [SerializeField]bool canDodge;

    [SerializeField] float dodgeSpeed;

    [SerializeField] float dodgeCD;

    [SerializeField]float dodgeTime;


    [Header("Jump")]
    public bool canJump;

    [SerializeField]float  maxFallingSpeed;

    [SerializeField]float jumpForce;


    [Header("Interact")]
    public IInteractible closestInteractObject;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parameters = GetComponent<Parameters>();
    }

    void Update()
    {
        if(!canPerformActions) return;

        Move();
        Jump();
        Dodge();
        Interact();


        Direction newDirection = Direction.Right;

        if(moveVector.x > 0){
            newDirection = Direction.Right;
            Flip(newDirection);
        }
        else if(moveVector.x < 0){
            newDirection = Direction.Left;
            Flip(newDirection);
        }
    }

    void Move()
    {
        if(!canMove) return;

        moveVector.x = Input.GetAxis("Horizontal");


        rb.linearVelocity = new Vector2(moveVector.x * parameters.moveSpeed * parameters.moveSpeedModifier, rb.linearVelocity.y);        
        if(rb.linearVelocity.y < -maxFallingSpeed){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallingSpeed);
        }
    }

    void Flip(Direction newDirection){
        if(newDirection == lookDirection){
            transform.localScale = new Vector3(1, 1, 1);
        }
        else{
            transform.localScale = new Vector3(-1, 1, 1);
        }

        lookDirection = newDirection;
    }

    void Jump()
    {
        if(!canJump) return;

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(new Vector2(0, 1) * jumpForce);
            canJump = false;
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
        canPerformActions = false;

        rb.gravityScale = 0;

        if(lookDirection == Direction.Right)
        {
            rb.linearVelocity = new Vector2(1, 0) * dodgeSpeed;
        }
        else if(lookDirection == Direction.Left){
            rb.linearVelocity = new Vector2(-1, 0) * dodgeSpeed;
        }
        
        yield return new WaitForSeconds(dodgeTime);
        
        canMove = true;

        canPerformActions = true;
        
        rb.gravityScale = 1;

        rb.linearVelocity = new Vector2(0, 0);

        yield return new WaitForSeconds(dodgeCD);

        canDodge = true;
    }
    
    void Interact(){
        if(Input.GetKeyDown(KeyCode.E)){
            closestInteractObject.Interact();
        }
    }
}

enum Direction{
    Right,
    Left
}
