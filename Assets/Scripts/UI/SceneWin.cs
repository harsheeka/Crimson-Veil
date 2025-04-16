using System.Collections;
using UnityEngine;

public class Win : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip carDoor;
    public AudioClip carRunning;
    private void Start()
    {
        StartCoroutine(PlaySounds());
    }

    private void Update()
    {
        if (Input.anyKey)
            LoadingHelper.LoadScene(Scenes.Menu);
    }

    private IEnumerator PlaySounds()
    {
        audioSource.clip = carDoor;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;

        audioSource.clip = carRunning;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;
    }
}
