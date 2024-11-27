using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _player, _aim;
    [SerializeField] private JoystickGetVector _joystick;
    [Range(100.0f, 400.0f)]
    [SerializeField] private int _movementSpeed = 4;
    [SerializeField] private float _maxRadius = 5f;

   
    private void Start()
    {
        ResetAimPos();
    }
    private void Update()
    {
        MoveAim();
    }
    private void MoveAim()
    {
            if (_joystick.ReturnInputJostickVector() != Vector3.zero)
            {
                _aim.SetActive(true);
  
           
                transform.Translate(_joystick.ReturnInputJostickVector() * _movementSpeed * Time.deltaTime);

                Vector3 newPosition = transform.position + _joystick.ReturnInputJostickVector() * _movementSpeed * Time.deltaTime;
                Vector3 offset = newPosition - _player.transform.position;

                if (offset.magnitude <= _maxRadius)
                {
                    transform.Translate(_joystick.ReturnInputJostickVector() * _movementSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = _player.transform.position + offset.normalized * _maxRadius;
                }
        }
            else
            {
                _aim.SetActive(false);
                ResetAimPos();
            }
    }

    private void ResetAimPos()
    {
        gameObject.transform.position = _player.transform.position;
    }
}
