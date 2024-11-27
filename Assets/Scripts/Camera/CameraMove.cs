using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _targetToMove;
    [SerializeField] private CameraShake _cameraShake;
    [Range(0, 300)]
    [SerializeField] private float _zOffset, _yOffset;

    private void LateUpdate()
    {
        if (_targetToMove != null && !_cameraShake.isShaking)
        {
            OffsetMoving();
        }
    }

    private void OffsetMoving()
    {
        Vector3 newPosition = _targetToMove.position;
        newPosition.z -= _zOffset;
        newPosition.y += _yOffset;
        transform.position = newPosition;
    }
}