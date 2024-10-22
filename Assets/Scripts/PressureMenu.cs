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

    private Vector3 _originalPosition;
    private Vector3 _objectOriginalPosition;
    private Color _originalColor;
    private bool _isActivated;
    private bool _objectActivated;
    private bool _soundPlayed;
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
        if (!other.CompareTag("Player") && !other.CompareTag("Interactable")) return;
        _isActivated = true;
        PlaySound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Interactable")) return;
        _isActivated = false;
        _objectActivated = false;
        _soundPlayed = false;
    }

    private void Update()
    {
        // Bewege die Platte und ändere ihre Farbe
        var targetPlatePosition = _isActivated ? pressedPosition : _originalPosition;
        plate.position = Vector3.Lerp(plate.position, targetPlatePosition, Time.deltaTime * moveSpeed);

        var targetColor = _isActivated ? pressedColor : _originalColor;
        _plateRenderer.material.color = Color.Lerp(_plateRenderer.material.color, targetColor, Time.deltaTime * moveSpeed);

        // Überprüfe, ob die Platte an ihrer Zielposition ist
        if (_isActivated && !_objectActivated && Vector3.Distance(plate.position, pressedPosition) <= activationThreshold)
        {
            _objectActivated = true;
        }

        // Bewege das Objekt zur Zielposition
        var targetObjectPosition = _isActivated ? targetPosition : _objectOriginalPosition;
        objectToMove.position = Vector3.Lerp(objectToMove.position, targetObjectPosition, Time.deltaTime * objectMoveSpeed);

        // Überprüfe, ob die Platte und das zu bewegende Objekt ihre Zielpositionen erreicht haben und beende das Spiel
        if (!_objectActivated || !(Vector3.Distance(plate.position, pressedPosition) <= activationThreshold)) return;
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void PlaySound()
    {
        if (audioSource == null || _soundPlayed) return;
        audioSource.Play();
        _soundPlayed = true;
    }
    
}
