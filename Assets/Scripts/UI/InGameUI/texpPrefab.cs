using UnityEngine;

public class texpPrefab : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3,3), 1));
        Destroy(gameObject, 2f);
    }
}
