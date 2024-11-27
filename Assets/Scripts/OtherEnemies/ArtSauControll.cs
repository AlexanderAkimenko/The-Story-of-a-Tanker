
using UnityEngine;
using System.Collections;


public class ArtSauControll : MonoBehaviour
{
    [SerializeField] private RegionActive _regionActive;
    [SerializeField] private Transform _player, _toward;
    [SerializeField] private float _rotationSpeed = 10.0f, _reloadTime = 15.0f;
    [SerializeField] private ParticleSystem _shootVFX;
    [SerializeField] private GameObject _shadow, _powerUPHP;
    [SerializeField] private SoundRocket _soundRocket;

    private Vector3 _rotationToPlayer;
    private bool startShoot;

    private void Update()
    {
        LookToPlayer();  
    }


    private void LookToPlayer()
    {
        if (_player != null && _regionActive._active)
        {
            _rotationToPlayer = _player.transform.position - _toward.position;
            _rotationToPlayer.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(_rotationToPlayer);
            _toward.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            if (!startShoot)
            {
                startShoot = true;
                StartCoroutine(ArtShoot());
            }
        }
    }

    private void ActiveVFX() => _shootVFX.Play();

    IEnumerator ArtShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_reloadTime);
            _soundRocket.RocketTakeoff();
            ActiveVFX();
            ActiveShadowArty();
        }

    }
    private void ActiveShadowArty() => Instantiate(_shadow,_toward.transform.position, Quaternion.identity);

    private void OnDisable()
    {
        if (_powerUPHP !=null)
        {
            _powerUPHP.SetActive(true);
            StopCoroutine(ArtShoot());
        }
       
    }

}
