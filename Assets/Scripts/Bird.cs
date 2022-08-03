using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _wasLaunched;
    private Rigidbody2D _rigidbody2D;
    private LineRenderer _lineRenderer;
    private float _timeSittingAround;
    
    [SerializeField] private float forceMultiplier = 500;
    [SerializeField] private float minBoundX = -10;
    [SerializeField] private float maxBoundX = 10;
    [SerializeField] private float minBoundY = -10;
    [SerializeField] private float maxBoundY = 10;
    [SerializeField] private float maxTimeSittingAround = 3;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(1, _initialPosition);
        
        if (_wasLaunched && _rigidbody2D.velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        var currentPosition = transform.position;
        
        _lineRenderer.SetPosition(0, currentPosition);

        if (!(currentPosition.y > maxBoundY) && !(currentPosition.y < minBoundY) && !(currentPosition.x > maxBoundX) &&
            !(currentPosition.x < minBoundX) && !(_timeSittingAround > maxTimeSittingAround)) return;
        
        var activeScene = SceneManager.GetActiveScene();
        var activeSceneName = activeScene.name;

        SceneManager.LoadScene(activeSceneName);
    }

    private void OnMouseDrag()
    {
        var mainCamera = Camera.main;

        if (!mainCamera)
        {
            return;
        }
        
        var mousePosition = Input.mousePosition;
        var newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        _wasLaunched = false;
        _timeSittingAround = 0;
        _lineRenderer.enabled = true;
    }

    private void OnMouseUp()
    {
        var currentPosition = transform.position;
        var direction = _initialPosition - currentPosition;
        
        _rigidbody2D.AddForce(direction * forceMultiplier);
        _rigidbody2D.gravityScale = 1;
        _wasLaunched = true;
        _lineRenderer.enabled = false;
    }
}
