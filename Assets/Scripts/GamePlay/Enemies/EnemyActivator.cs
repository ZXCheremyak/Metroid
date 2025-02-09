using UnityEngine;
using System;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    void Start(){
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius, layerMask);
        foreach(Collider2D coll in colls){
            if(coll.CompareTag("Player"))
            {
                SetComponentsActivity(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            Debug.Log("Detected");
            SetComponentsActivity(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.CompareTag("Player"))
        {
            SetComponentsActivity(false);
        }
    }

    void SetComponentsActivity(bool value){
        try{
            GetComponentInParent<Enemy>().enabled = value;
        }
        catch(Exception e){
            Debug.Log(e);
        }
    }
}
