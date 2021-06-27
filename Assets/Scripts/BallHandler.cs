using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.IsPressed()) return;
        
        var touchPosition =  Touchscreen.current.primaryTouch.position.ReadValue();

        Debug.Log(touchPosition);
    }
}
