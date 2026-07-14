using UnityEngine;

[CreateAssetMenu(fileName = nameof(ScreenData), menuName = nameof(ScreenData))]
public class ScreenData : ScriptableObject
{
    public float PixelPerUnit { get; private set; }

    private void OnEnable()
    {
        Camera camera = Camera.main;

        PixelPerUnit = camera.pixelHeight / (camera.orthographicSize * 2f);
    }
}