using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Vector3 targetPosition;  
    public Vector3 targetRotation;  
    public float transitionDuration = 1.0f; 

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private bool _isSwitched;
    private bool _isTransitioning;
    private float _transitionTime;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _endPosition;
    private Quaternion _endRotation;

    private void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isTransitioning)
        {
            if (_isSwitched)
            {
                _startPosition = targetPosition;
                _startRotation = Quaternion.Euler(targetRotation);
                _endPosition = _originalPosition;
                _endRotation = _originalRotation;
            }
            else
            {
                _startPosition = _originalPosition;
                _startRotation = _originalRotation;
                _endPosition = targetPosition;
                _endRotation = Quaternion.Euler(targetRotation);
            }

            _transitionTime = 0f;
            _isTransitioning = true;
            _isSwitched = !_isSwitched;
        }

        if (!_isTransitioning) return;
        _transitionTime += Time.deltaTime;
        var t = _transitionTime / transitionDuration;

        transform.position = Vector3.Lerp(_startPosition, _endPosition, t);
        transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);

        if (t >= 1.0f)
        {
            _isTransitioning = false;
        }
    }
}
