using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public KeyCode flashlightKey = KeyCode.F;
    [Header(UnityInspector.Sound)]
    public Light lightSource;
    [Header(UnityInspector.Sound)]
    public AudioSource flashlightAudio;
    public AudioClip onSound;
    public AudioClip offSound;

    void Update()
    {
        // Toggle flashlight on key press
        if (Input.GetKeyDown(flashlightKey))
        {
            lightSource.enabled = !lightSource.enabled;
            // Play corresponding sound
            flashlightAudio.clip = lightSource.enabled ? onSound : offSound;
            flashlightAudio.Play();
        }
    }
}
