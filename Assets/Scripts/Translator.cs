using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Translator : MonoBehaviour
{
    [SerializeField]
    private string _ru;
    
    [SerializeField]
    private string _en;

    [SerializeField]
    private string _tr;

    private TextMeshProUGUI textMPro;

    private void Awake()
    {
        textMPro = GetComponent<TextMeshProUGUI>();
        SwithcLanguage(YandexGame.savesData.language);
    }

    private void OnEnable() => YandexGame.SwitchLangEvent += SwithcLanguage;

    private void OnDisable() => YandexGame.SwitchLangEvent -= SwithcLanguage;

    private void SwithcLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                textMPro.text = _ru;
                break;
            case "tr":
                textMPro.text = _tr;
                break;
            default:
                textMPro.text = _en;
                break;
        }
    }
}

