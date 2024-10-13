using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Vector3 targetPosition;  
    public Vector3 targetRotation;  
    public float transitionDuration = 1.0f; 

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isSwitched = false;
    private bool isTransitioning = false;
    private float transitionTime = 0f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 endPosition;
    private Quaternion endRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isTransitioning)
        {
            if (isSwitched)
            {
                startPosition = targetPosition;
                startRotation = Quaternion.Euler(targetRotation);
                endPosition = originalPosition;
                endRotation = originalRotation;
            }
            else
            {
                startPosition = originalPosition;
                startRotation = originalRotation;
                endPosition = targetPosition;
                endRotation = Quaternion.Euler(targetRotation);
            }

            transitionTime = 0f;
            isTransitioning = true;
            isSwitched = !isSwitched;
        }

        if (isTransitioning)
        {
            transitionTime += Time.deltaTime;
            float t = transitionTime / transitionDuration;

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            if (t >= 1.0f)
            {
                isTransitioning = false;
            }
        }
    }
}
