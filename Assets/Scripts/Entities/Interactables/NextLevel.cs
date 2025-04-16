using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [Header(UnityInspector.Interaction)]
    public KeyCode interactKey = KeyCode.E;

    private void OnTriggerStay(Collider other)
    {
        //while player is nearby the door and presses the interact key, tries to open the door.
        if (Input.GetKeyDown(interactKey)) LoadingHelper.LoadScene(Scenes.Win);
    }

    private void OnTriggerEnter(Collider other)
    {
        UIIngameText.SetCustomText("Exit");
    }

    private void OnTriggerExit(Collider other)
    {
        UIIngameText.ClearText();
    }
}
