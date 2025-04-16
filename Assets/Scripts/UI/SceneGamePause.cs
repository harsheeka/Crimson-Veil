using UnityEngine;

//managing the pause menu functionality, including pausing and resuming the game.
public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pauseMenuUI;
    public FirstPersonController player;
    [Header(UnityInspector.Sound)]
    public AudioSource pauseAudioSource;
    public AudioClip pauseMusic;
    [Header(UnityInspector.Debug)]
    [ReadOnly][SerializeField] private bool _paused = false;
    private void Awake()
    {
        //setting music to pause audio source.
        pauseAudioSource.clip = pauseMusic;

        //showing Pause Menu UI only if the game is paused.
        pauseMenuUI.SetActive(_paused);
    }

    private void Update()
    {
        if (_paused)
            pauseAudioSource.UnPause();
        else
            pauseAudioSource.Pause();

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            PauseResume();
    }

    private void PauseResume() //toggling game pause state
    {
        _paused = !_paused;
        pauseMenuUI.SetActive(_paused);
        player.cameraCanMove = !_paused;
        Time.timeScale = _paused ? 0f : 1f;
        var audios = FindObjectsOfType<AudioSource>();
        foreach (var audio in audios)
        {
            if (_paused)
                audio.Pause();
            else
                audio.UnPause();
        }
    }
}
