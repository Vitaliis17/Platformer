using UnityEngine;

[CreateAssetMenu(fileName = nameof(ScreenData), menuName = nameof(ScreenData))]
public class ScreenData : ScriptableObject, IPixelPerUnitSender
{
    public Vector2 PixelPerUnit { get; private set; }

    private void OnEnable()
    {
        Camera camera = Camera.main;

        float height = camera.pixelHeight / (camera.orthographicSize * 2f);
        float width = camera.pixelWidth / (camera.orthographicSize * 2f);

        PixelPerUnit = new Vector2(width, height);
    }
}