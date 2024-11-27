using UnityEngine;
public class PowerUp : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _vfxPU;
    public PowerUpTypes PowerUpType;
    public enum PowerUpTypes
    
    {
        HealthBoosts,
        ReloadBoosts,
        PlayerSpeedBoosts,
        ArmorActive
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.enabled = true;
            if (_vfxPU != null)
            {
                _vfxPU.Stop();
            }
        }
    }

}
