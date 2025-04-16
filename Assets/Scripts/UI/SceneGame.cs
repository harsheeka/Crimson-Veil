using UnityEngine;
public class SceneGame : MonoBehaviour
{
    public Color fogColor = new(10, 10, 10, 255);
    public Color ambientColor = new(10, 10, 10);
    private void Start()
    {
        RenderSettings.fogColor = fogColor;
        RenderSettings.ambientLight = ambientColor;
    }
}
