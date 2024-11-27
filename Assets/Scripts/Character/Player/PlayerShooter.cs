using UnityEngine;
using System.Collections;

public class PlayerShooter : BaseTank, IShootable
{
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected Transform _towardTransform;
    [SerializeField] private ParticleSystem _shootVFX;
    private Coroutine _shootingCoroutine;

  protected override void Start()
  {
      base.Start();
        PlayerEvents.OnStartShooting.AddListener(StartShooting);
        PlayerEvents.OnStopShooting.AddListener(StopShooting);
    }

private void StartShooting()
    {
        if (_shootingCoroutine == null)
        { 
            _shootingCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

   private void StopShooting()
    {
      
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
                  _shootingCoroutine = null;
        }
    }

   private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            if (PowerUpButtonControl._reloadBoostsActive)
            {
                yield return new WaitForSeconds(_tankConfig.ReloadingTime - (_tankConfig.ReloadingTime * 25 / 100));
            }
            else
            {
                yield return new WaitForSeconds(_tankConfig.ReloadingTime);
            }
        
        }
    }

    public void Shoot()
    {
        if (_objectPooler != null)
        {
            PlayerEvents.PlayerOnShoot?.Invoke();
            _objectPooler.SpawnFromPoll(_tankConfig.BulletTag, _shootPoint.position, _towardTransform.rotation);
            _shootVFX.Play();
        }
        else Debug.Log("nullObjectpooll");
    }
    private void OnEnable()
    {
        PlayerEvents.OnStartShooting.RemoveListener(StartShooting);
        PlayerEvents.OnStopShooting.RemoveListener(StopShooting);
        PlayerEvents.PlayerOnShoot.RemoveAllListeners();
    }
}
