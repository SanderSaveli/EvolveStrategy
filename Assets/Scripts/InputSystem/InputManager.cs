using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singletone<InputManager>
{
    #region Events
    public delegate void StartLeftMouseTouch(Vector3 position, float time);
    public event StartLeftMouseTouch OnStartLeftMouseTouch;

    public delegate void EndLeftMouseTouch(Vector3 position, float time);
    public event EndLeftMouseTouch OnEndLeftMouseTouch;

    public delegate void StartRightMouseTouch(Vector3 position, float time);
    public event StartRightMouseTouch OnStartRightMouseTouch;

    public delegate void EndRightMouseTouch(Vector3 position, float time);
    public event EndRightMouseTouch OnEndRightMouseTouch;

    public delegate void CursorPositionChanged(Vector3 position);
    public event CursorPositionChanged OnCursorPositionChanged;
    #endregion

    public Vector3 cursorPosition { get; private set; }

    private Camera mainCamera => Camera.main;
    private TouchControl inputActions;

    private void Awake()
    {
        inputActions = new TouchControl();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Start()
    {
        inputActions.Touch.LeftMouseContact.started += ctx => StartLeftMouseContact(ctx);
        inputActions.Touch.LeftMouseContact.canceled += ctx => EndLeftMouseContact(ctx);

        inputActions.Touch.RightMouseContact.started += ctx => StartRightMouseContact(ctx);
        inputActions.Touch.RightMouseContact.canceled += ctx => EndRightMouseContact(ctx);
    }
    public void StartWork()
    {
        inputActions = new TouchControl();
        inputActions.Enable();

        inputActions.Touch.LeftMouseContact.started += ctx => StartLeftMouseContact(ctx);
        inputActions.Touch.LeftMouseContact.canceled += ctx => EndLeftMouseContact(ctx);

        inputActions.Touch.RightMouseContact.started += ctx => StartRightMouseContact(ctx);
        inputActions.Touch.RightMouseContact.canceled += ctx => EndRightMouseContact(ctx);
    }
    private void Update()
    {
        Vector3 newCursorPosition = GetCursorPosition();
        if (cursorPosition != newCursorPosition)
        {
            cursorPosition = newCursorPosition;
            OnCursorPositionChanged?.Invoke(cursorPosition);
        }
    }

    private void StartLeftMouseContact(InputAction.CallbackContext context)
    {
        OnStartLeftMouseTouch?.Invoke(cursorPosition, (float)context.startTime);
    }

    private void EndLeftMouseContact(InputAction.CallbackContext context)
    {
         OnEndLeftMouseTouch?.Invoke(cursorPosition, (float)context.time);
    }

    private void StartRightMouseContact(InputAction.CallbackContext context)
    {
        OnStartRightMouseTouch?.Invoke(cursorPosition, (float)context.startTime);
    }

    private void EndRightMouseContact(InputAction.CallbackContext context)
    {
        OnEndRightMouseTouch?.Invoke(cursorPosition, (float)context.time);
    }

    private Vector3 GetCursorPosition() 
    {
        Vector3 contactPosition = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
        return mainCamera.ScreenToWorldPoint(contactPosition);
    }
}
