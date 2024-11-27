using UnityEngine;

public class CameraShake : MonoBehaviour
{
   [SerializeField] private float _shakeDuration = 2f;
   [SerializeField] private float _shakeMagnitude = 3f;
    private float elapsed = 0f;
    public bool isShaking = false; 

    public void Shake(float duration,float magnitude)
    {
        _shakeDuration = duration;
        _shakeMagnitude = magnitude;
        isShaking = true;
    }
    public void Shake()
    {
        isShaking = true;
    }

    private void LateUpdate()
    {
        if (elapsed < _shakeDuration && isShaking)
        {
         
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            transform.localPosition += new Vector3(x, y, 0); 

            elapsed += Time.deltaTime;
        }
        else
        {
            elapsed = 0;
            isShaking = false;
        }
    }
}
