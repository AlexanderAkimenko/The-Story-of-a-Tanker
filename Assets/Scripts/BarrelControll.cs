using System.Collections;
using UnityEngine;


public class BarrelControll : MonoBehaviour
{
    [SerializeField] private GameObject _arty;
    [SerializeField] private GameObject[] _explosionsArty;
    [SerializeField] private ParticleSystem _parentParticle;
    private int _countHits = 0;

    private float _explTime;
    private bool _firstExplosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null  )
        {
            if (other.GetComponent<Bullet>().GetTankTag() == "Player")
            {
                _countHits++;
            }
        }
    }


    private void Update()
    {
        if (_countHits >= 3 && !_firstExplosion)
        {
            _firstExplosion = true;
          ExplosionArty();
          if (!_parentParticle.IsAlive())
          {
              Debug.Log("is a live");
          }
        }
    }

    private void ExplosionArty()
    {
        foreach (var explosion in _explosionsArty)
        {
            explosion.SetActive(true);
        }

        StartCoroutine(DeactiveBarrel());

    }

    private  IEnumerator DeactiveBarrel()
    {
        yield return new WaitForSeconds(1);
        DestroyBarrel();
    }
    
    private void DestroyBarrel()
    {
        Debug.Log("DestroyBarrel");
        foreach (var explosion  in _explosionsArty)
        {
            if (explosion.transform.parent.gameObject.activeInHierarchy)
            {
                explosion.transform.parent.gameObject.SetActive(false);
            }
            _arty.SetActive(false);
           
        }
    }
}