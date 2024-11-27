using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StartRocket : MonoBehaviour
{
    [SerializeField] private Animator _rocketAnim;
    [SerializeField] private HealthTank _healthTank;
    [SerializeField] private float _percentageForActivation = 90;

    private Coroutine _rocketActive;

    void Update()
    {
        if (_healthTank.GetHPInProcent() <= _percentageForActivation && _rocketActive == null)
        {
          _rocketActive = StartCoroutine(ActiveRocket());
        }

        IEnumerator ActiveRocket()
        {
            while(true)
            {
               
                _rocketAnim.SetTrigger("RocketShoot");
                Debug.Log("StartShoot");
                yield return new WaitForSeconds(20); 
            }
        }
    }
    
}
