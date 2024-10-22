using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform plate;
    public Vector3 pressedPosition;
    public Color pressedColor = Color.red;
    public Transform objectToMove;
    public Vector3 targetPosition;
    public float moveSpeed = 2f;
    public float objectMoveSpeed = 2f;
    public float activationThreshold = 0.1f;
    public AudioSource audioSource;
    public bool staysActivated = true;

    private Vector3 _originalPosition;
    private Vector3 _objectOriginalPosition;
    private Color _originalColor;
    private bool _isActivated;
    private bool _soundPlayed;
    private bool _isAnimating;
    private Renderer _plateRenderer;

    private void Start()
    {
        _originalPosition = plate.position;
        _objectOriginalPosition = objectToMove.position;
        _plateRenderer = plate.GetComponent<Renderer>();
        _originalColor = _plateRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAnimating || (!other.CompareTag("Player") && !other.CompareTag("Interactable")) || _isActivated) return;
        _isActivated = true;
        PlaySound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isAnimating && !staysActivated && (other.CompareTag("Player") || other.CompareTag("Interactable")))
        {
            _isActivated = false;
        }
    }

    private void Update()
    {
        // Set target positions and color based on the activation state
        var targetPlatePosition = _isActivated ? pressedPosition : _originalPosition;
        var targetObjectPosition = _isActivated ? targetPosition : _objectOriginalPosition;
        var targetColor = _isActivated ? pressedColor : _originalColor;

        // Smoothly move the plate and object towards their target positions
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);
        objectToMove.position = Vector3.Lerp(objectToMove.position, targetObjectPosition, Time.deltaTime * objectMoveSpeed);

        // Smoothly transition the plate's color
        _plateRenderer.material.color = Color.Lerp(_plateRenderer.material.color, targetColor, Time.deltaTime * moveSpeed);
    }

    private void PlaySound()
    {
        if (audioSource == null || _soundPlayed) return;
        audioSource.Play();
        _soundPlayed = true;
    }

    public void StartAnimation()
    {
        _isAnimating = true;
    }

    public void EndAnimation()
    {
        _isAnimating = false;
    }
}
