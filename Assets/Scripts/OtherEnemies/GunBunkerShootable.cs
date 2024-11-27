using UnityEngine;
using System.Collections;

public class GunBunkerShootable : MonoBehaviour, IShootable
{
    [SerializeField] private Transform _player, _checkRotatoin, _shootPosition;
    [SerializeField] private float _speedRotation = 10f;
    [SerializeField] private float _minRotation = Mathf.Deg2Rad * -65f;
    [SerializeField] private float _maxRotation = Mathf.Deg2Rad * 65f;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private ParticleSystem _shootVFX;

    private ObjectPooler _objectPooler;
    private Vector3 _gunDirection;
    private Coroutine _shootingCoroutine;
    private bool IsALookToPlayer;
    private RaycastHit _hit;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }
    private void FixedUpdate()
    {
        LookToPlayer();
        RaycastBunkerControll();
    }


    private void RaycastBunkerControll()
    {
        if (Physics.Raycast(_shootPosition.position, _shootPosition.forward, out _hit, Mathf.Infinity))
        {
            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.DrawRay(_shootPosition.position, _shootPosition.forward * _hit.distance, Color.blue);

                if (_shootingCoroutine == null)
                {
                    _shootingCoroutine = StartCoroutine(StartShootBunker());

                    IsALookToPlayer = true;
                }

            }
            else
            {
                if (_shootingCoroutine != null)
                {
                    StopCoroutine(_shootingCoroutine);
                    _shootingCoroutine = null;
                }
                IsALookToPlayer = false;
                Debug.DrawRay(_shootPosition.position, _shootPosition.forward * _hit.distance, Color.red);

            }
        }

    }


    public void Shoot()
    {
        if (_objectPooler != null)
        {
            _objectPooler.SpawnFromPoll("RedBulletBunker", _shootPosition.position, transform.rotation);
            _shootVFX.Play();
        }
        else Debug.Log("null Objectpooll");
    }

   

    private void LookToPlayer()
    {
        if (_player != null)
        {
            _gunDirection = (_player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(_gunDirection);

            float currentYRotationInRadian = Mathf.Deg2Rad * _checkRotatoin.transform.eulerAngles.y;
            float targetYRotationInRadian = Mathf.Deg2Rad * targetRotation.eulerAngles.y;
            float angleDifference = Mathf.DeltaAngle(Mathf.Rad2Deg * currentYRotationInRadian, Mathf.Rad2Deg * targetYRotationInRadian);
         
      
            if (currentYRotationInRadian + angleDifference > _minRotation && currentYRotationInRadian + angleDifference < _maxRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
            }
        }
    }

    IEnumerator StartShootBunker()
    {
        while (IsALookToPlayer)
        {

            yield return new WaitForSeconds(_reloadingTime);
            Shoot();
        }

    }
}
