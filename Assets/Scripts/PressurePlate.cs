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

    private Vector3 originalPosition;
    private Vector3 objectOriginalPosition;
    private Color originalColor;
    private bool isActivated = false;
    private bool soundPlayed = false;
    private bool isAnimating = false;
    private Renderer plateRenderer;

    void Start()
    {
        originalPosition = plate.position;
        objectOriginalPosition = objectToMove.position;
        plateRenderer = plate.GetComponent<Renderer>();
        originalColor = plateRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAnimating && (other.CompareTag("Player") || other.CompareTag("Interactable")) && !isActivated)
        {
            isActivated = true;
            PlaySound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isAnimating && !staysActivated && (other.CompareTag("Player") || other.CompareTag("Interactable")))
        {
            isActivated = false;
        }
    }

    void Update()
    {
        // Set target positions and color based on the activation state
        Vector3 targetPlatePosition = isActivated ? pressedPosition : originalPosition;
        Vector3 targetObjectPosition = isActivated ? targetPosition : objectOriginalPosition;
        Color targetColor = isActivated ? pressedColor : originalColor;

        // Smoothly move the plate and object towards their target positions
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);
        objectToMove.position = Vector3.Lerp(objectToMove.position, targetObjectPosition, Time.deltaTime * objectMoveSpeed);

        // Smoothly transition the plate's color
        plateRenderer.material.color = Color.Lerp(plateRenderer.material.color, targetColor, Time.deltaTime * moveSpeed);
    }

    private void PlaySound()
    {
        if (audioSource != null && !soundPlayed)
        {
            audioSource.Play();
            soundPlayed = true;
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;
    }

    public void EndAnimation()
    {
        isAnimating = false;
    }
}
