using UnityEngine;
using System.Collections;
using System;

public class PressureMenu : MonoBehaviour
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

    private Vector3 originalPosition;
    private Vector3 objectOriginalPosition;
    private Color originalColor;
    private bool isActivated = false;
    private bool objectActivated = false;
    private bool soundPlayed = false;
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
        if (other.CompareTag("Player") || other.CompareTag("Interactable"))
        {
            isActivated = true;
            PlaySound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Interactable"))
        {
            isActivated = false;
            objectActivated = false;
            soundPlayed = false;
        }
    }

    void Update()
    {
        // Bewege die Platte und ändere ihre Farbe
        Vector3 targetPlatePosition = isActivated ? pressedPosition : originalPosition;
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);

        Color targetColor = isActivated ? pressedColor : originalColor;
        plateRenderer.material.color = Color.Lerp(plateRenderer.material.color, targetColor, Time.deltaTime * moveSpeed);

        // Überprüfe, ob die Platte an ihrer Zielposition ist
        if (isActivated && !objectActivated && Vector3.Distance(plate.position, pressedPosition) <= activationThreshold)
        {
            objectActivated = true;
        }

        // Bewege das Objekt zur Zielposition
        Vector3 targetObjectPosition = isActivated ? targetPosition : objectOriginalPosition;
        objectToMove.position = Vector3.Lerp(objectToMove.position, targetObjectPosition, Time.deltaTime * objectMoveSpeed);

        // Überprüfe, ob die Platte und das zu bewegende Objekt ihre Zielpositionen erreicht haben und beende das Spiel
        if (objectActivated && Vector3.Distance(plate.position, pressedPosition) <= activationThreshold)
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && !soundPlayed)
        {
            audioSource.Play();
            soundPlayed = true;
        }
    }
    
}
