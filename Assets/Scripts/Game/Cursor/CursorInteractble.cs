using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public static class CursorInteractble
{
    private static ControllerInput _inputActions;

    public static void EnableInput()
    {
        _inputActions = new();
        _inputActions.Cursor.Click.performed += Click;
        _inputActions.Enable();
    }

    private static void Click(InputAction.CallbackContext callbackContext)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.transform.TryGetComponent(out Plate plate))
        {
            plate.SetIngradientCooking();
            YandexGame.FullscreenShow();
        }
    }

    public static void DisableInput()
    {
        _inputActions.Disable();
    }
    
    public static void DestroyInput()
    {
        _inputActions.Cursor.Click.performed -= Click;
    }
}
