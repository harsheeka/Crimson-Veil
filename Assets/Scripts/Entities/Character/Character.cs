using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour   
{
    public FirstPersonController firstPersonController; //reference to the FirstPersonController script
    public float currentLife = 100f;
    public const float maxLife = 100f;
    private const float minLife = 0f;
    [Header("Voice")]
    public AudioSource voiceSource; //reference to the AudioSource component for voice sounds
    public AudioClip breathing; //sound clip for breathing
    public AudioClip pain; //sound clip for pain
    [Header("Heartbeat")]
    public AudioSource heartbeatSource; //heartbeat sound source
    public AudioClip heartbeat; //heartbeat sound clip
    [Header("Footsteps")]
    public AudioSource footstepsSource; 
    public AudioClip footsteps;

    [Header("Damage")]

    public Image DamageIndicator; //image to indicate damage

    public bool queuePainSound = false;

    private void Start()
    {
        DamageIndicator.gameObject.SetActive(true); //enable the damage indicator image
    }

    private void Update()
    {
        CalculateDamageIndicatorAlpha();
        Breathing(); 
        Footsteps();
        Heartbeat();

        //check if the player is dead and load the death scene if so.
        if (currentLife < minLife)
            SceneManager.LoadScene($"{Scenes.Death}");
    }

    private void CalculateDamageIndicatorAlpha()
    {
        //player's life inversely proportional transparency alpha percentage.
        float alphaPercentage = 1 - (currentLife / maxLife);

        DamageIndicator.color = new Color(1f, 1f, 1f, alphaPercentage);
    }

    private void Heartbeat()
    {
        heartbeatSource.clip = heartbeat;
        heartbeatSource.enabled = currentLife < 60f;
        heartbeatSource.pitch = (currentLife / 100f) switch
        {
            <= 0.1f => 1.60f, //less or equal to 10%.
            <= 0.3f => 1.40f, //less or equal to 30%.
            _ => 1.15f //more than 30%.
        };
    }

    private void Breathing() //breathing sounds of charc
    {
        if (!queuePainSound)
        {
            //breathing sound when sprinting
            voiceSource.enabled = firstPersonController.isSprinting || firstPersonController.sprintRemaining < firstPersonController.sprintDuration;
            voiceSource.clip = breathing;

            if (firstPersonController.isSprinting) //player is sprinting - breathing sound - faster
                voiceSource.pitch = 1.5f;
            else
                voiceSource.pitch = 1.0f;
        }
    }

    private void Footsteps() //footsteps
    {
        footstepsSource.clip = footsteps; //if walking enable
        footstepsSource.enabled = firstPersonController.isWalking;

        if (firstPersonController.isSprinting) //when sprinting - footsteps sound - faster
            footstepsSource.pitch = 1.5f;
        else
            footstepsSource.pitch = 1.0f;
    }

    public void Damage(float damageToInflict) //aaplying damange to chracter
    {
        currentLife = currentLife >= minLife ? currentLife - damageToInflict : minLife;
        StartCoroutine(PainSound());
    }

    private IEnumerator PainSound()
    {
        queuePainSound = true;
        voiceSource.clip = pain;
        voiceSource.Play();
        while (voiceSource.isPlaying) yield return null;
        queuePainSound = false;
    }
}
