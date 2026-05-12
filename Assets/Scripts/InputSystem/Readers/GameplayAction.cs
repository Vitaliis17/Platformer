using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class GameplayAction : ActionMap, IMovementReader, ITouchReader, IJumpReader
{
    private readonly Subject<Vector2> _directionChanged = new();
    private readonly Subject<bool> _pressChanged = new(); 
    private readonly Subject<bool> _jumped = new();

    private InputSystem_Actions.GameplayActions _action;

    private Vector2 _movementDirection;

    public Observable<Vector2> DirectionChanged => _directionChanged;
    public Observable<bool> PressChanged => _pressChanged;
    public Observable<bool> Jumped => _jumped;

    private void Awake()
    {
        _action = new InputSystem_Actions().Gameplay;
        Map = _action;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _action.Touch.performed += ActivatePressing;
        _action.Touch.canceled += DeactivatePressing;

        _action.Jumping.performed += ActivateJumping;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _action.Touch.performed -= ActivatePressing;
        _action.Touch.canceled -= DeactivatePressing;

        _action.Jumping.performed -= ActivateJumping;
    }

    private void Update()
        => _movementDirection = _action.Movement.ReadValue<Vector2>();

    private void FixedUpdate()
        => _directionChanged.OnNext(_movementDirection);

    private void OnDestroy()
    {
        _directionChanged?.Dispose();
        _pressChanged?.Dispose();

        _jumped?.Dispose();
    }

    private void ActivatePressing(InputAction.CallbackContext context)
        => _pressChanged.OnNext(true);

    private void ActivateJumping(InputAction.CallbackContext context)
        => _jumped.OnNext(true);

    private void DeactivatePressing(InputAction.CallbackContext context)
        => _pressChanged.OnNext(false);
}