using System.Collections;
using TMPro;
using UnityEngine;

public class UITipText : MonoBehaviour
{
    [Header("Hint")]
    public TextMeshProUGUI tipText;
    public HintType hintType;
    public float displayDuration = 3.0f;

    [Header("Show Once")]
    public bool showOnce = true;

    [ReadOnly][SerializeField] private bool _alreadyShown = false;
    private string _text = string.Empty;
    private readonly int _fadeTime = 1;

    private IEnumerator FadeInAndOutText(string str = "")
    {
        _text = str;
        tipText.text = _text;

        yield return StartCoroutine(FadeHelper.OverlayFadeIn(tipText, _fadeTime));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeHelper.OverlayFadeOut(tipText, _fadeTime));

        _alreadyShown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!showOnce || (showOnce && !_alreadyShown))
            StartCoroutine(FadeInAndOutText(GetHint()));
    }

    private string GetHint()
    {
        return hintType switch
        {
            HintType.Flashlight => $@"Press ""{KeyCode.F}"" to use the Flashlight.",
            HintType.Sprint => $@"Press ""Shift"" to use the Sprint.",
            HintType.Crouch => $@"Press ""Ctrl"" to use the Crouch.",
            HintType.Interact => $@"Press ""{KeyCode.E}"" to Interact.",
            HintType.FindExit => "Find the Exit!",
            _ => throw new System.NotImplementedException(),
        };
    }

    public enum HintType
    {
        Flashlight,
        Sprint,
        Crouch,
        Interact,
        FindExit
    }
}
