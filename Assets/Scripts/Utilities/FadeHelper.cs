using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class FadeHelper
{
    public static IEnumerator OverlayFadeIn(Graphic graphicToFade, float duration) => Fade(FadeType.FadeIn, duration, graphicToFade);
    public static IEnumerator OverlayFadeOut(Graphic graphicToFade, float duration) => Fade(FadeType.FadeOut, duration, graphicToFade);
    private static IEnumerator Fade(FadeType fadeType, float fadeDuration, Graphic graphicToFade)
    {
        var targetedTransparency = fadeType == FadeType.FadeIn ? 1.0f : 0.0f;
        float elapsedTime = 0;
        Color color = graphicToFade.color;
        float startTransparency = color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentTransparency = Mathf.Lerp(startTransparency, targetedTransparency, elapsedTime / fadeDuration);
            color.a = currentTransparency;
            graphicToFade.color = color;
            yield return null;
        }

        color.a = targetedTransparency;
        graphicToFade.color = color;
    }

    public enum FadeType
    {
        FadeIn,
        FadeOut
    }
}
