using System.Collections;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
[SerializeField] private float  _timeToSpawn;
[SerializeField] private GameObject _car;
    
    void Start()
    {
        StartCoroutine(StartCarSpawn());
    }

    IEnumerator StartCarSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            _car.SetActive(true);
        }
      
    }
 
}
