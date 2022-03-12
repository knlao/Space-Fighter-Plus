using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public bool doShake;
    
    private Vector3 _originalPos;
	
    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
	
    private void OnEnable()
    {
        _originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (doShake)
        {
            camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
        }
    }

    public void ShakeCam(bool shake)
    {
        doShake = shake;
    }
}