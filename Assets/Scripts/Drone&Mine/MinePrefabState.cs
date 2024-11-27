using System;
using UnityEngine;

public class MinePrefabState : MonoBehaviour
{
   [SerializeField] private GameObject _minePrefab;
   [SerializeField] private PipeMoving _pipeMoving;
 

   private ObjectPooler _objectPooler;
   private void OnEnable()
   {
      _pipeMoving.MineExplosion.AddListener(PrefabDeactivated);
   
   }

   private void Start()
   {
      _objectPooler = ObjectPooler.Instance;
   }

   public void PrefabActive() => _minePrefab.SetActive(true);

   
   public void PrefabDeactivated()
   {
   
      _objectPooler.SpawnFromPoll("BoomVfx", _minePrefab.transform.position, Quaternion.identity);
      _objectPooler.SpawnFromPoll("ExhaustPipe", _minePrefab.transform.position, Quaternion.Euler(90,0,0));
   
      _minePrefab.SetActive(false);
   }

   private void OnDisable()
   {
      _pipeMoving.MineExplosion.RemoveListener(PrefabDeactivated);
   }
}
