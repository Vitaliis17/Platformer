using UnityEngine;
using R3;
using Zenject;

public class MovementStickActivateSwitcher : MonoBehaviour
{
    [Inject(Id = TriggerNames.MovementStick)] private IHaveTriggerEvent _event;
    [Inject(Id = TriggerNames.MovementStick)] private IActivitySetter _activitySetter;

    private void Start()
    {
        _event.IsTriggered
        .Subscribe(isTriggered => SetActivity(isTriggered))
        .AddTo(this);
    }

    private void SetActivity(bool isTriggered)
    {
        if (isTriggered)
        {
            _activitySetter.Deactivate();

            return;
        }

        _activitySetter.Activate();
    }
}