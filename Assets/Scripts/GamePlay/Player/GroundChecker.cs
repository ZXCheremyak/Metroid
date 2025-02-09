using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Parameters parameters;

    [SerializeField]int collidersTouched;

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Ground")){
            collidersTouched++;
            parameters.isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Ground")){
            collidersTouched--;
            if(collidersTouched == 0)
            {
                parameters.isGrounded = false;
            }
        }
    }
}
