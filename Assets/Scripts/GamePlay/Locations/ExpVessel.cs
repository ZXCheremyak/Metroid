using UnityEngine;

public class ExpVessel : MonoBehaviour, IHitable
{
    [SerializeField] int expReward;
    bool broken;
    public void TakeDamage(float damage, Element Element){
        if(broken) return;
        Break();
    }

    void Break(){
        EventManager.GainExp.Invoke(expReward);
        this.enabled = false;
        broken = true;
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var psemission = ps.emission;
        psemission.rateOverTime = 0;
    }
}
