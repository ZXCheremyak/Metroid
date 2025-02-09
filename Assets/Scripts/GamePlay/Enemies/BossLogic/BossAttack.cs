using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    [SerializeField] protected Element element;

    protected virtual void Start(){
        this.enabled = false;
    }

    public abstract void PerformAttack(int attackIndex);
}
