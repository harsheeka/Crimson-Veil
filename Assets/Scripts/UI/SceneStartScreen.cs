using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [Header(UnityInspector.Sound)]
    public AudioSource startMenuSound;

    [Header("Fade")]
    public Image fadeOverlay;
    public float fadeOutTime = 1.5f;
    public float afterFadeWait = 2;

    [Header(UnityInspector.Debug)]
    [SerializeField] private bool _starting = false;

    private Color _fadeOutImageColor;

    private void Start()
    {
        Color transparentColor;
        transparentColor = _fadeOutImageColor = fadeOverlay.color;
        transparentColor.a = 0;
        fadeOverlay.color = transparentColor;
        fadeOverlay.gameObject.SetActive(_starting);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            if (!_starting)
            {
                _starting = true;
                StartCoroutine(StartTheGame());
            }
        }
    }

    private IEnumerator StartTheGame()
    {
        startMenuSound.Play();
        fadeOverlay.gameObject.SetActive(_starting);
        yield return StartCoroutine(FadeHelper.OverlayFadeIn(fadeOverlay, fadeOutTime));
        yield return new WaitForSeconds(afterFadeWait);
        LoadingHelper.LoadScene(Scenes.Game);
        _starting = false;
    }
}
