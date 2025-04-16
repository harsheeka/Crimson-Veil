using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitLoading());
    }

    private IEnumerator WaitLoading()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene($"{LoadingHelper.SceneToLoad}");
    }
}
