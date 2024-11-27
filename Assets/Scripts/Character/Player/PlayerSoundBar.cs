
using UnityEngine;

public class PlayerSoundBar : MonoBehaviour
{
    [SerializeField] private AudioClip _shootPlayer;
    [SerializeField] private AudioSource _audioSource;


    private void Start()
    {
        PlayerEvents.PlayerOnShoot.AddListener(ShootPlayerSound);
    }

    private void ShootPlayerSound() => _audioSource.PlayOneShot(_shootPlayer, 0.1f);

    void OnDestroy()
    {
        PlayerEvents.PlayerOnShoot.RemoveListener(ShootPlayerSound);
    }
}
