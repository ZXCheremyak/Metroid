using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool destroysOnPlayerCollision = true;
    public float damage;
    public Element element;

    void Start(){
        Destroy(gameObject, 5f);
        if(GetComponent<Rigidbody2D>().linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.TryGetComponent<Parameters>(out Parameters target) && coll.gameObject.CompareTag("Player"))
        {
            if(!destroysOnPlayerCollision) return;
            Destroy(gameObject);
            
            target.TakeDamage(damage, element);
            return;
        }
        Destroy(gameObject);
    }
}
