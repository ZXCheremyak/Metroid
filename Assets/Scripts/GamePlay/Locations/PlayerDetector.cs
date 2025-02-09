using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    void Start(){
        Collider2D coll = Physics2D.OverlapCircle(transform.position, GetComponent<CircleCollider2D>().radius, playerLayer);
        if(coll == null) return;
        GetComponentInParent<Enemy>().playerDetected = true;
        GetComponentInParent<Enemy>().player = coll.GetComponent<PlayerBase>();
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            try{
                GetComponentInParent<Enemy>().playerDetected = true;
                GetComponentInParent<Enemy>().player = coll.GetComponent<PlayerBase>();
            }
            catch(Exception e){
                Debug.Log(e);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.CompareTag("Player"))
        {
            try{
                GetComponentInParent<Enemy>().playerDetected = false;
                GetComponentInParent<Enemy>().player = null;
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
