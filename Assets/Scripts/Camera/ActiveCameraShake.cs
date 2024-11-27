using UnityEngine;

public class ActiveCameraShake : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _duration, _magnitude;

    public void TriggerShakeBoss()
    {
        _cameraShake.Shake();
    }
    public void ShakeRocket()
    {
        _cameraShake.Shake(_duration, _magnitude);
    }
}
