using UnityEngine.UI;
using UnityEngine;
using R3;

public class FirebaseSaveLoaderView : MonoBehaviour, IHaveOnClickEvent
{
    [SerializeField] private Button _saveButton;

    public Observable<Unit> Clicked { get; private set; }

    private void Awake()
        => Clicked = _saveButton.OnClickAsObservable();
}