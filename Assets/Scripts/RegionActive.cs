using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionActive : MonoBehaviour
{
    [SerializeField]  List<EnemyPatrol> _enemyPatorlRegion = new List<EnemyPatrol>();
    [SerializeField] private GameObject[] _gameObjects;

    public bool _active;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WallTrigger>() != null && !_active)
        {
            foreach (var item in _enemyPatorlRegion)
            {
                item.enabled = true;
                EnemyPatrol._playerDetection = true;
             //   Debug.Log("region Active");

                _active = true;
            }

            if (_gameObjects != null)
            {
                foreach (var obj in _gameObjects)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
