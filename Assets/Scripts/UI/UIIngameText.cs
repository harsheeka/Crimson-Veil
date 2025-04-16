using TMPro;
using UnityEngine;

public class UIIngameText : MonoBehaviour
{
    [Header("In Game")]
    public GameObject inGameTextUI;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI tipText;

    private static string _text = string.Empty;

    private void Start()
    {
        inGameTextUI.SetActive(true);
    }

    private void Update()
    {
        interactionText.text = _text;
    }

    public static void SetCustomText(string text)
        => UIIngameText._text = text;

    public static void ClearText() => _text = string.Empty;

    public static void SetInteractionText(KeyCode keyCode, string interaction)
        => _text = $@"Press ""{keyCode}"" to {interaction}";
}
