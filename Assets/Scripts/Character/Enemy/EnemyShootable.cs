using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootable : BaseTank, IShootable
{
    [Header("Стрельба")]
 
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _towardTransform;
    [SerializeField] private EnemyPatrol _enemyAgent;
    [SerializeField] private ParticleSystem _shootVFX;

    private Coroutine _shootingCoroutine;
    private RaycastHit _hit;
    private float _reloadingTime;
    protected override void Start()
    {
        base.Start();
        _reloadingTime = _tankConfig.ReloadingTime;
    }

    private void Update()
    {
        if (EnemyPatrol._playerDetection)
        {
            RaycastEnemyControll();
        }
      
    }
    private void RaycastEnemyControll()
    {
        if (Physics.Raycast(_shootPoint.position, _shootPoint.forward, out _hit, Mathf.Infinity))
        {
            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.DrawRay(_shootPoint.position, _shootPoint.forward * _hit.distance, Color.blue);

                if (_shootingCoroutine == null)
                {
                    _shootingCoroutine = StartCoroutine(StartShoot());

                    _enemyAgent.IsALookToPlayer = true;
                }

            }
            else
            {
                if (_shootingCoroutine != null)
                {
                    StopCoroutine(_shootingCoroutine);
                    _shootingCoroutine = null;
                }
                _enemyAgent.IsALookToPlayer = false;
                Debug.DrawRay(_shootPoint.position, _shootPoint.forward * _hit.distance, Color.red);

            }
        }

    }

    public void Shoot()
    {
        if (_objectPooler != null)
        {
            _objectPooler.SpawnFromPoll(_tankConfig.BulletTag, _shootPoint.position, _towardTransform.rotation);
            _shootVFX.Play();
        }
        else Debug.Log("null Objectpooll");
    }

    IEnumerator StartShoot()
    {
        while(_enemyAgent.IsALookToPlayer)
        {
            yield return new WaitForSeconds(_reloadingTime);
            Shoot();
        }
      
    }
}
