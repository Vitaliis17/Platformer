using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _aspectRatio;

    [SerializeField] private Transform _map;

    private void Awake()
    {
        Vector2 newAspectRatio = new(_camera.pixelWidth, _camera.pixelHeight);

        float oldCoefficient = _aspectRatio.x / _aspectRatio.y;
        float newCoefficient = newAspectRatio.x / newAspectRatio.y;

        float newScaleX = newCoefficient / oldCoefficient * transform.localScale.x;

        _map.localScale = new(newScaleX, _map.localScale.y);
    }
}