using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//managing the death sequence,displaying images and playing death sounds before going to the game over screen
public class Death : MonoBehaviour
{
    [Header("Images")]
    public Image deathImage1;
    public Image deathImage2;

    [Header(UnityInspector.Sound)]
    public AudioSource audioSource;
    public AudioClip deathSound1;
    public AudioClip deathSound2;
    public AudioClip monsterBite;

    private void Start()
    {
        deathImage1.gameObject.SetActive(false);
        deathImage2.gameObject.SetActive(false);

        //begin the death sequence
        StartCoroutine(PlayDeathSounds());
    }

    private IEnumerator PlayDeathSounds()
    {
        audioSource.clip = deathSound1;
        audioSource.Play();
        deathImage1.gameObject.SetActive(true);
        while (audioSource.isPlaying) yield return null;

        audioSource.clip = deathSound2;
        audioSource.Play();
        deathImage2.gameObject.SetActive(true);
        while (audioSource.isPlaying) yield return null;

        //final sound before transition
        audioSource.clip = monsterBite;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;

        SceneManager.LoadScene($"{Scenes.GameOver}");
    }
}
