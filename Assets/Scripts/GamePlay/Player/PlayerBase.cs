using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Ground"))
        {
            GetComponent<Movement>().canJump = true;
        }

        if(coll.gameObject.TryGetComponent<ICollectible>(out ICollectible collectibleObj))
        {
            collectibleObj.Collect();
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.TryGetComponent<IInteractible>(out IInteractible interactibleObject))
        {
            GetComponent<Movement>().closestInteractObject = interactibleObject;
        }
    }
}
