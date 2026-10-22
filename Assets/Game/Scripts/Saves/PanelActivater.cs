using UnityEngine;

public class PanelActivater : MonoBehaviour, IActivater
{
    [SerializeField] private Transform _panel;

    public void Activate()
    {
        Debug.Log("Activate");
        _panel.gameObject.SetActive(true);
    }
}