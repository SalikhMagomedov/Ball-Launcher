using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float detachDelay = .5f;
    [SerializeField] private float respawnDelay = 1f;

    private SpringJoint2D _currentBallSpringJoint;
    private Rigidbody2D _currentBallRigidbody;
    private Camera _mainCamera;
    private bool _isDragging;

    private void Start()
    {
        _mainCamera = Camera.main;
        SpawnNewBall();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (!_currentBallRigidbody) return;

        if (Touch.activeTouches.Count == 0)
        {
            if (_isDragging) LaunchBall();

            _isDragging = false;

            return;
        }

        _isDragging = true;
        _currentBallRigidbody.isKinematic = true;

        var touchPositions = new Vector2();
        foreach (var touch in Touch.activeTouches)
        {
            touchPositions += touch.screenPosition;
        }
        touchPositions /= Touch.activeTouches.Count;
        
        var worldPosition = _mainCamera.ScreenToWorldPoint(touchPositions);

        _currentBallRigidbody.position = worldPosition;
    }

    private void SpawnNewBall()
    {
        var ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        _currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        _currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();
        _currentBallSpringJoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        _currentBallRigidbody.isKinematic = false;
        _currentBallRigidbody = null;
        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;
        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}