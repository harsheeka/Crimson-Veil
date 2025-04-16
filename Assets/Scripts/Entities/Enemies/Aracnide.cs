using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Aracnide : MonoBehaviour
{
    public Character player;
    [Header("Audio")]
    public AudioSource biteAudioSource;
    public AudioClip biteAudioClip;
    public float proximityThreshold = 3.0f;

    [Header("Enemy Behavior")]
    public float baseDamage = 20f;
    public bool canAttackPlayer = true;
    public bool canChasePlayer = true;
    public AracnideHitboxChase aracnideHitboxChase;
    [Header(UnityInspector.Debug)]
    [SerializeField] private bool _isPlayingSound;
    [SerializeField] private bool _isChasingPlayer;
    private NavMeshAgent _aracnideNavMeshAgent;

    private void Start()
    {
        _aracnideNavMeshAgent = GetComponent<NavMeshAgent>();
        biteAudioSource.clip = biteAudioClip;
    }

    private void FixedUpdate()
    {
        // Chase the player if within range
        if (canChasePlayer && aracnideHitboxChase.onChaseRadius)
            _aracnideNavMeshAgent.SetDestination(player.transform.position);
    }

    private IEnumerator PlayMonsterBiteSound()
    {
        _isPlayingSound = true;
        biteAudioSource.Play();
        yield return new WaitForSeconds(biteAudioSource.clip.length + 1.0f);
        _isPlayingSound = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Attack the player on collision
        if (canAttackPlayer)
        {
            StartCoroutine(PlayMonsterBiteSound());
            player.Damage(baseDamage);
        }
    }
}
