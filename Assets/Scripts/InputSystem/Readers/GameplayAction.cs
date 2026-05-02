using UnityEngine;
using R3;

public class GameplayAction : ActionMap
{
    private readonly Subject<Vector2> _directionChanged = new();

    private InputSystem_Actions.GameplayActions _action;

    private Vector2 _movementDirection;

    public Observable<Vector2> DirectionChanged => _directionChanged;

    private void Awake()
    {
        _action = new InputSystem_Actions().Gameplay;
        Map = _action;
    }

    private void Update()
        => _movementDirection = _action.Movement.ReadValue<Vector2>();

    private void FixedUpdate()
        => _directionChanged.OnNext(_movementDirection);

    private void OnDestroy()
        => _directionChanged?.Dispose();
}
