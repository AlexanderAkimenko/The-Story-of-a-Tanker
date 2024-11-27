
using UnityEngine;
using Random = UnityEngine.Random;

public class ActiveShadowRocket : MonoBehaviour
{
    [SerializeField] private GameObject _shadow;
    [SerializeField] private GameObject[] _spawnPoints;
  

    private int _currentShadow = 0;

    static public bool RocketActive, FirstHit;

    private void Awake()
    {
        Debug.Log(gameObject.name);
        RocketActive = false;
        FirstHit = false;
    }

    void LateUpdate()
    {
        if (_currentShadow >=2)
        {
            FirstHit = false;
        }
        if ( FirstHit)
        {
            
            RocketAttack();
        }
        if (RocketActive)
        {
            _currentShadow = 0;
            RocketAttack();
        }
 

    }
   private void RocketAttack()
    {
        CreateRocket();

            RocketActive = false;
        FirstHit = false;

    }
    private void CreateRocket()
    {
        GameObject rocket = Instantiate(_shadow, _spawnPoints[
             Random.Range(0, _spawnPoints.Length)].transform.position, Quaternion.identity);
        _currentShadow++;
    }
}
