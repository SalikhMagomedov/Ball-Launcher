using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;
    [SerializeField] private float detachDelay = .5f;

    private Camera _mainCamera;
    private bool _isDragging;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!currentBallRigidbody) return;

        if (!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            if (_isDragging) LaunchBall();

            _isDragging = false;

            return;
        }

        _isDragging = true;
        currentBallRigidbody.isKinematic = true;

        var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}