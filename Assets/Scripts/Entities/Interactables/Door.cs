using System.Collections;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour //door interactions
{
    [Space(UnityInspector.SpaceDefault)]
    public bool Locked = true; //door locked state

    [Header(UnityInspector.Interaction)]
    public KeyCode interactKey = KeyCode.E; //E key to interact with the door
    public Transform playerTransform; //reference to the player transform
    public TextMeshProUGUI interactionText;

    [Header(UnityInspector.Sound)]
    public AudioSource doorAudioSource;
    public AudioClip doorUnlockedClip;
    public AudioClip doorLockedClip;

    [Header(UnityInspector.Animation)]
    private Animator animator;

    [Header(UnityInspector.Debug)]

    [ReadOnly][SerializeField] private bool _isOpen = false; //is the door open or closed
    [ReadOnly][SerializeField] private bool _isAnimationNotRunning;
    [ReadOnly][SerializeField] private bool _tryingToOpen = false;
    [ReadOnly][SerializeField] private int _tries = 0;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        //while player is nearby the door and presses the interact key, tries to open the door.
        if (Input.GetKeyDown(interactKey)) TryOpenDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        //updating door interactive text.
        UpdateDoorInteractiveText();
    }
    private void OnTriggerExit(Collider other)
    {
        //clearing the interaction text outside the collision box.
        UIIngameText.ClearText();
    }

    private void Update()
    {
        _isAnimationNotRunning = IsAnimationNotRunning("DoorClose", "DoorOpen");
    }
    private void TryOpenDoor()
    {
        if (!Locked)
        {
            if (IsAnimationNotRunning("DoorClose", "DoorOpen") && !_tryingToOpen)
            {
                _tries++;
                _tryingToOpen = true;

                animator.Play(_isOpen ? "DoorClose" : "DoorOpen");

                doorAudioSource.clip = doorUnlockedClip;
                doorAudioSource.Play();

                _isOpen = !_isOpen;
                UpdateDoorInteractiveText();

                StartCoroutine(EndTransitionAfterAnimation());
            }
        }
        else
        {
            doorAudioSource.clip = doorLockedClip;
            doorAudioSource.Play();
            UIIngameText.SetCustomText("Door Locked!");
        }
    }


    private void UpdateDoorInteractiveText() => UIIngameText.SetCustomText(_isOpen ? "Close" : "Open");
    private IEnumerator EndTransitionAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        _tryingToOpen = false;
    }
    private bool IsAnimationNotRunning(params string[] animations)
    {
        if (animator == null) return false;

        foreach (var animation in animations)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(animation) && stateInfo.normalizedTime < 1)
                return false;
        }

        return true;
    }
}
