using UnityEngine.SceneManagement;

public static class LoadingHelper
{
    public static Scenes SceneToLoad = Scenes.Menu;
    public static void LoadScene() => SceneManager.LoadScene($"{Scenes.Loading}");
    public static void LoadScene(Scenes scene)
    {
        SceneToLoad = scene;
        LoadScene();
    }
}
