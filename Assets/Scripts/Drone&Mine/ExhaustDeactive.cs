using UnityEngine;

public class ExhaustDeactive : MonoBehaviour
{
    [SerializeField] private ParticleSystem _exhaustVfx;

    

    void Update()
    {
        if (_exhaustVfx != null)
        {
            if (!_exhaustVfx.isPlaying)
            {
                gameObject.SetActive(false);
            }  
        }
        
    }
}
