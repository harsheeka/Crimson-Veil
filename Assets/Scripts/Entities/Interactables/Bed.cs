using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public bool canSleep = false;
    [Header(UnityInspector.Interaction)]
    [Header("Fade")]
    public Image fadeOutImage;
    [Header(UnityInspector.Debug)]
    private bool _isSleeping = false;
    public KeyCode interactKey = KeyCode.E;
    void Start() { }
    void Update() { }
    private void TryToSleep()
    {
        if (canSleep && !_isSleeping)
        {
            StartCoroutine(Sleep());
        }
        else
        {
            UIIngameText.SetCustomText(@"""I don't feel it's safe to sleep here.""");
        }
    }

    private IEnumerator Sleep()
    {
        _isSleeping = true;
        yield return new WaitForSeconds(2f);
        _isSleeping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateBedInteractiveText();
    }

    private void OnTriggerExit(Collider other)
    {
        UIIngameText.ClearText();
    }

    private void UpdateBedInteractiveText() => UIIngameText.SetCustomText("Sleep");
}
