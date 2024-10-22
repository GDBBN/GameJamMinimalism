using UnityEngine;

public class PressurePlay : MonoBehaviour
{
    public Transform plate;
    public Vector3 pressedPosition;
    public Color pressedColor = Color.red;
    public float moveSpeed = 2f;
    public float activationThreshold = 0.1f;
    public AudioSource audioSource;
    public Animator animator;
    public string animationTrigger = "clear"; 

    private Vector3 _originalPosition;
    private Color _originalColor;
    private bool _isActivated;
    private bool _soundPlayed;
    private bool _animationPlayed; 
    private Renderer _plateRenderer;

    private void Start()
    {
        _originalPosition = plate.position;
        _plateRenderer = plate.GetComponent<Renderer>();
        _originalColor = _plateRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!other.CompareTag("Player") && !other.CompareTag("Interactable")) || _isActivated) return;
        _isActivated = true;
        PlaySound();

        if (_animationPlayed) return;
        PlayAnimation(); 
        _animationPlayed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Interactable")) return;
        _isActivated = false;
        _soundPlayed = false;
        _animationPlayed = false;
    }

    private void Update()
    {
        var targetPlatePosition = _isActivated ? pressedPosition : _originalPosition;
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);

        var targetColor = _isActivated ? pressedColor : _originalColor;
        _plateRenderer.material.color = Color.Lerp(_plateRenderer.material.color, targetColor, Time.deltaTime * moveSpeed);
    }

    private void PlaySound()
    {
        if (audioSource == null || _soundPlayed) return;
        audioSource.Play();
        _soundPlayed = true;
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(animationTrigger);
        }
    }
}
