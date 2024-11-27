using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MineBlinking : MonoBehaviour
{
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private float _blinkInterval = 0.5f;
    [SerializeField] private AudioClip _blinkSound;
    [SerializeField] private Renderer _renderer; 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _boomVFX;
    private Material _defuaultMaterial;
    
    private bool _isBlinking = true;
    private bool _isRed = true;

    void OnEnable()
    {
        _defuaultMaterial = _renderer.material;
        if (_renderer == null)
        {
         
            Debug.LogError("Renderer not found!");
            enabled = false;
            return;
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource not found!");
            enabled = false;
            return;
        }
        else
        {
             _audioSource.PlayOneShot(_blinkSound, 0.5f);   
        }

       StartCoroutine(BlinkCoroutine());
    }

 

    IEnumerator BlinkCoroutine()
    {
        while (_isBlinking)
        {
            Debug.Log("Работает корутина");

            if (_isRed)
            {
                _renderer.material = _yellowMaterial;
                _isRed = false;
            }
            else
            {
                _renderer.material = _defuaultMaterial;
                _isRed = true;
            }

            yield return new WaitForSeconds(_blinkInterval);
        }
    }

    void OnDisable()
    {
        _isBlinking = false;
        
        
    }
}