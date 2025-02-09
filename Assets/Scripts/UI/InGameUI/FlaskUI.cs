using UnityEngine;
using UnityEngine.UI;

public class FlaskUI : MonoBehaviour
{
    [SerializeField] Image flaskChargeImage;

    void Start(){
        EventManager.ChangeFlaskCharge.AddListener(ChangeFlaskUI);
    }

    void ChangeFlaskUI(float flaskCharge, float maxFlaskCharge){
        flaskChargeImage.GetComponent<RectTransform>().sizeDelta = new Vector2(145 * flaskCharge / maxFlaskCharge, 6);
    }
}
