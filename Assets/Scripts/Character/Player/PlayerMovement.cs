using UnityEngine;

public class PlayerMovement : BaseTank
{
    [Header("Script Move")]
    [SerializeField] private JoystickGetVector _joystick;

    private Vector3 _targetPos;


     void FixedUpdate()
    {
        if (_joystick.ReturnInputJostickVector() != Vector3.zero)
        {

            Move(_joystick.ReturnInputJostickVector());
            RotateTankBody(_joystick.ReturnInputJostickVector());
        }
        else
        {
            _rigidbody.velocity -= new Vector3(_rigidbody.velocity.x -Time.deltaTime,_rigidbody.velocity.y, _rigidbody.velocity.z - Time.deltaTime);
        }
       
    }
  
    protected virtual void Move(Vector3 moveVector)
    {
        if (PowerUpButtonControl._speedBoostsActive)
        {
            _targetPos = (_rigidbody.position + moveVector * (_tankConfig.MovementSpeed + (_tankConfig.MovementSpeed * 30 /100)) * Time.fixedDeltaTime);
        }
        else
        {
            _targetPos = (_rigidbody.position + moveVector * _tankConfig.MovementSpeed * Time.fixedDeltaTime);
        }
    
        _rigidbody.velocity = (_targetPos - _rigidbody.position) / Time.fixedDeltaTime;
         
    }
    private void RotateTankBody(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 30 * Time.deltaTime);
    }
}   