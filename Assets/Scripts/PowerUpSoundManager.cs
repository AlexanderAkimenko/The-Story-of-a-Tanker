using UnityEngine;
using UnityEngine.Events;

public class PowerUpSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _addedPU, _hpBoostsActive;
    public UnityEvent AddedPU;

    private void Start()
    {
        AddedPU.AddListener(AddedPUSoundPlay);
    }
    private void AddedPUSoundPlay()
    {
        _audioSource.PlayOneShot(_addedPU, 0.2f);
    }
    public void HPBoostsSoundPlay()
    {
        _audioSource.PlayOneShot(_hpBoostsActive, 0.2f);
    }

}
