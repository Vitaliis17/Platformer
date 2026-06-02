using UnityEngine;

[CreateAssetMenu(fileName = "ScreenData", menuName = "ScreenData")]
public class ScreenData : ScriptableObject
{
    public float PixelPerUnit { get; private set; }

    private void Awake()
    {
        Camera camera = Camera.main;

        PixelPerUnit = camera.pixelHeight / (camera.orthographicSize * 2f);
    }
}