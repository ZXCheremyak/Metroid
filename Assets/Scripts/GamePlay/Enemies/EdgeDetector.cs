using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            enemy.closeToEdge = true;
        }
    }
}
