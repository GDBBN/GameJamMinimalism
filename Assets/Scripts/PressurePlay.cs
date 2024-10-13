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

    private Vector3 originalPosition;
    private Color originalColor;
    private bool isActivated = false;
    private bool soundPlayed = false;
    private bool animationPlayed = false; 
    private Renderer plateRenderer;

    void Start()
    {
        originalPosition = plate.position;
        plateRenderer = plate.GetComponent<Renderer>();
        originalColor = plateRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Interactable")) && !isActivated)
        {
            isActivated = true;
            PlaySound();

            if (!animationPlayed)
            {
                PlayAnimation(); 
                animationPlayed = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Interactable"))
        {
            isActivated = false;
            soundPlayed = false;
            animationPlayed = false;
        }
    }

    void Update()
    {
        Vector3 targetPlatePosition = isActivated ? pressedPosition : originalPosition;
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);

        Color targetColor = isActivated ? pressedColor : originalColor;
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

    private void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(animationTrigger);
        }
    }
}
