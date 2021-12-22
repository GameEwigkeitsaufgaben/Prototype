using UnityEngine;

public class LiftCaveShake : MonoBehaviour
{
    private Vector3 _lastPosition;
    private Vector3 _lastRotation;

    public float shake = 0.01f;
    public float TraumaExponent = 1;
    [Tooltip("Maximum angle that the gameobject can shake. In euler angles.")]
    public Vector3 MaximumAngularShake = Vector3.one * 5;
    [Tooltip("Maximum translation that the gameobject can receive when applying the shake effect.")]
    public Vector3 MaximumTranslationShake = Vector3.one * .9f;

    public bool shakeCabine = false;

    // Update is called once per frame
    void Update()
    {
        if (shakeCabine)
        {
            StartShake();
        }
      
    }

    public void StartShake()
    {
        shakeCabine = true;
        var previousRotation = _lastRotation;
        var previousPosition = _lastPosition;
        /* In order to avoid affecting the transform current position and rotation each frame we substract the previous translation and rotation */
        _lastPosition = new Vector3(
            MaximumTranslationShake.x * (Mathf.PerlinNoise(0, Time.time * 25) * 2 - 1),
            MaximumTranslationShake.y * (Mathf.PerlinNoise(1, Time.time * 25) * 2 - 1),
            MaximumTranslationShake.z * (Mathf.PerlinNoise(2, Time.time * 25) * 2 - 1)
        ) * shake;

        _lastRotation = new Vector3(
            MaximumAngularShake.x * (Mathf.PerlinNoise(3, Time.time * 25) * 2 - 1),
            MaximumAngularShake.y * (Mathf.PerlinNoise(4, Time.time * 25) * 2 - 1),
            MaximumAngularShake.z * (Mathf.PerlinNoise(5, Time.time * 25) * 2 - 1)
        ) * shake;

        transform.localPosition += _lastPosition - previousPosition;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + _lastRotation - previousRotation);
    }

    public void StopShake()
    {
        shakeCabine = false;
    }
}
