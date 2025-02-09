using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.TryGetComponent<Parameters>(out Parameters parameters))
        {
            Debug.Log("Died");
            parameters.Die();
        }
    }
}
