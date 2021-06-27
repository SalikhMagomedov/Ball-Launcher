using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.IsPressed()) return;
        
        var touchPosition =  Touchscreen.current.primaryTouch.position.ReadValue();
        var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

        Debug.Log(worldPosition);
    }
}
