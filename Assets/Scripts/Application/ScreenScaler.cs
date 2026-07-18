using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _aspectRatio;

    [SerializeField] private Transform[] _transforms;

    private void Awake()
    {
        Vector2 newAspectRatio = new(_camera.pixelWidth, _camera.pixelHeight);

        float coefficientX = newAspectRatio.x / _aspectRatio.x * _aspectRatio.y / newAspectRatio.y;
        float coefficientY = newAspectRatio.y / _aspectRatio.y * _aspectRatio.x / newAspectRatio.x;

        foreach (Transform transform in _transforms)
            transform.localScale = new(transform.localScale.x * coefficientX, transform.localScale.y * coefficientY);
    }
}