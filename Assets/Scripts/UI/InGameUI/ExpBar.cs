using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Image expBarImage;

    void Start(){
        EventManager.ChangeExp.AddListener(ChangeExpUI);
    }

    void ChangeExpUI(int value, int maxValue){
        expBarImage.GetComponent<RectTransform>().sizeDelta = new Vector2(145 * (float)value / (float)maxValue, 6);
    }
}

