using System;
using UnityEngine;

public class FallRocket : MonoBehaviour
{
    [SerializeField] private Rigidbody _rocketRb;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fallRocket;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private GameObject _shadowRocket, _explosionRocket;


    private SoundRocket _soundRocket;
 
    public bool StartFall;
    private Vector3 _currentPos;
    private void Start()
    {
        _soundRocket = FindObjectOfType<SoundRocket>();
        _currentPos = transform.position;
    }
    private void FixedUpdate()
    {
        if (StartFall)
        {
            _rocketRb.AddForce(Vector3.down * _fallSpeed * 50.0f * Time.fixedDeltaTime);
            _rocketRb.useGravity = true;
            _audioSource.PlayOneShot(_fallRocket, 0.01f);
        }
        else
        {
            _currentPos = new Vector3(_shadowRocket.transform.position.x,
                transform.position.y, _shadowRocket.transform.position.z);
            _rocketRb.useGravity = false;
            transform.position = _currentPos;
     

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         
            HealthTank health = other.GetComponent<HealthTank>();

            if (health != null)
            {
                health.ChangeHealth(30);

            }
        }
        if (other.gameObject.CompareTag("ShadowRocket"))
        {
      
            if (!ActiveShadowRocket.FirstHit)
            {
                ActiveShadowRocket.FirstHit = true;
            
            }
        
            Instantiate(_explosionRocket, _shadowRocket.transform.position + new Vector3(0,5,0),Quaternion.identity);
            
            StartFall = false;
            if (_soundRocket != null)
            {
                _soundRocket.ExplosionRocket?.Invoke();
            }
            Destroy(_shadowRocket);
           

        }
    }
    public void TriggerFall()
    {
        StartFall = true;
    }

 
}
