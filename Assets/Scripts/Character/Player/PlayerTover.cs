using UnityEngine;


public class PlayerTover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float rotationSpeed = 5f;

    private Vector3 _distanceToTarget;
    private float _distance;
    [Range(20.0f, 50.0f)]
    private float _limitLook = 30.0f;

    void Update()
    {
        if (_target != null)
        {
            _distanceToTarget =gameObject.transform.position - _target.position;
            _distance = _distanceToTarget.magnitude;

            if (_distance > _limitLook)
            {
                Vector3 targetDirection = _target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x,0,targetDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        
        }
    }
}
