
using System;
using UnityEngine;
using UnityEngine.Events;

public class SoundRocket : MonoBehaviour
{
    [SerializeField] private AudioSource RocketAudioSource;
    [SerializeField] private AudioClip _firstTakeOff, explosionSound;

    [HideInInspector] public UnityEvent ExplosionRocket = new UnityEvent();
    private void OnEnable()
    {
        ExplosionRocket.AddListener(ExplosionActive);
    }

    public void RocketTakeoff()
    {
        RocketAudioSource.PlayOneShot(_firstTakeOff,0.05f);
    }

    public void ExplosionActive()
    {
        RocketAudioSource.PlayOneShot(explosionSound, 0.01f);
    }

    private void OnDisable()
    {
        ExplosionRocket.RemoveAllListeners();
    }
}
