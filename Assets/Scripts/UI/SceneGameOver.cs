using System.Collections;
using TMPro;
using UnityEngine;

//manageing the game over screen functionality, including retrying and returning to the main menu.
public class GameOver : MonoBehaviour
{
    [Header("Buttons")]
    public TextMeshProUGUI retry;
    public TextMeshProUGUI menu;

    [Header(UnityInspector.Sound)]
    public AudioSource audioSource;
    public AudioClip clickSound;
    private void Start()
    {
        audioSource.clip = clickSound;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Retry()
    {
        StartCoroutine(PlaySound());
        LoadingHelper.LoadScene(Scenes.Game);
    }
    public void Menu()
    {
        StartCoroutine(PlaySound());
        LoadingHelper.LoadScene(Scenes.Menu);
    }
    private IEnumerator PlaySound()
    {
        audioSource.Play();

        //wait for 1 second before continuing.
        yield return new WaitForSeconds(1);
    }
}
