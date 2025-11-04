using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 상태를 보여준다
public class HUD : MonoBehaviour
{
    // 레벨, 직업, 경험치, 생명력, 스태미나, 포만감 등의 상태 표시
    public enum InfoType { Level, Occupation, Exp, Hp, Stamina, Satiety, Quench }
    public InfoType infoType;

    TextMeshProUGUI textMeshProUGUI;
    Slider slider;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }
}
