using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;

    void Start()
    {
        EventManager.ChangePlayerHealth.AddListener(SetHealthBarSize);
    }

    void SetHealthBarSize(float hp, float maxHp){
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(145 * hp / maxHp, 6);
    }
}
