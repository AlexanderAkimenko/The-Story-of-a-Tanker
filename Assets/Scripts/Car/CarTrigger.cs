using System;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector3 _startPos;
    
    public event Action CarDestroy;
    public event Action KilledByAPlayer; 
    private void OnEnable()
    {
        _startPos = transform.position;
        if (_animator != null)
        {
            _animator.Rebind();
            _animator.Update(0f);
        }
    }

    private void OnDisable()
    {
        transform.position = _startPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if ( bullet != null)
        {
            if ("Player" == bullet.GetTankTag())
            { 
                CarDestroy?.Invoke(); 
                KilledByAPlayer?.Invoke();
                DeactiveCar();
                
                // added particle
            }
        }

        if (other.gameObject.CompareTag("EndOfTheRoad"))
        {
            CarDestroy?.Invoke(); 
            DeactiveCar();
        }
    
    }

    private void DeactiveCar()
    {
        gameObject.SetActive(false);
    }
}
