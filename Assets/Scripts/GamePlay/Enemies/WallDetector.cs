using UnityEngine;

public class WallDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ground"))
        {
            GetComponentInParent<Enemy>().closeToEdge = true;
        }
    }
}
