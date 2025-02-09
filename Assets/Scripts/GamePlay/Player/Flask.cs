using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField]float flaskCharge;

    [SerializeField]float healing;

    [SerializeField]float maxFlaskCharge;
    [SerializeField] AudioClip flaskSound;

    void Start()
    {
        EventManager.enemyDamaged.AddListener(RefillFlask);
        EventManager.CheckPointUsed.AddListener(RestoreCharge);
        EventManager.ChangeFlaskCharge.Invoke(flaskCharge, maxFlaskCharge);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            RecoverHealth();
        }
    }

    void RefillFlask(){
        flaskCharge += 5;
        if(flaskCharge > maxFlaskCharge)
        {
            flaskCharge = maxFlaskCharge;
        }
        EventManager.ChangeFlaskCharge.Invoke(flaskCharge, maxFlaskCharge);
    }

    void RecoverHealth(){
        if(flaskCharge < 50) return;
        GetComponent<Parameters>().RecoverHealth(healing);
        flaskCharge -= 50;
        EventManager.ChangeFlaskCharge.Invoke(flaskCharge, maxFlaskCharge);
        EventManager.PlaySound.Invoke(flaskSound);
    }

    void RestoreCharge(CheckPoint c){
        flaskCharge = maxFlaskCharge;
        EventManager.ChangeFlaskCharge.Invoke(flaskCharge, maxFlaskCharge);
    }
}
