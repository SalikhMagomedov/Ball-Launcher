using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            currentBallRigidbody.isKinematic = false;
            return;
        }
        
        currentBallRigidbody.isKinematic = true;
        
        var touchPosition =  Touchscreen.current.primaryTouch.position.ReadValue();
        var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
    }
}
