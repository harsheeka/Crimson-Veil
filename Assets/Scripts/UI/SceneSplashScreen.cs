using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenFade : MonoBehaviour //managing the splash screen fade in and fade out functionality, including loading the next scene.
{
    [Header("Fade")]
    public Scenes SceneToLoad;
    public Image fadeImage;
    public float fadeInTime = 5.0f;
    public float fadeOutTime = 5.0f;
    public float waitTimeDuration = 5.0f;
    public bool loadingTransition = true;

    [Header(UnityInspector.Debug)]
    [SerializeField] private bool _playing = false;
    [SerializeField] private bool _playingFadeIn = false;
    [SerializeField] private bool _playingWaitTime = false;
    [SerializeField] private bool _playingFadeOut = false;

    private void Start() 
    {
        StartCoroutine(StartTheGame());
    }

    private IEnumerator StartTheGame()
    {
        _playing = true;
        fadeImage.gameObject.SetActive(_playing);

        yield return StartCoroutine(FadeIn());

        if (!_playingFadeIn)
            yield return StartCoroutine(WaitTime());

        if (!_playingWaitTime)
            yield return StartCoroutine(FadeOut());

        if (loadingTransition) LoadingHelper.LoadScene(SceneToLoad);
        else SceneManager.LoadScene($"{SceneToLoad}");

        _playing = false;
    }

    private IEnumerator WaitTime()
    {
        _playingWaitTime = true;
        yield return new WaitForSeconds(waitTimeDuration);
        _playingWaitTime = false;
    }

    private IEnumerator FadeIn()
    {
        _playingFadeIn = true;
        yield return FadeHelper.OverlayFadeOut(fadeImage, fadeInTime);
        _playingFadeIn = false;
    }

    private IEnumerator FadeOut()
    {
        _playingFadeOut = true;
        yield return FadeHelper.OverlayFadeIn(fadeImage, fadeOutTime);
        _playingFadeOut = false;
    }
}
