using UnityEngine;

public class MineActivation : MonoBehaviour
{
 [SerializeField] private DronePatrol _dronePatrol;
 [SerializeField] private GameObject _minePrefab;
 private void OnEnable()
 {
     if (_dronePatrol !=null)
     {
         _dronePatrol.MineActivate.AddListener(Activated);
     }
 }

 private void Activated( int index)
 {
     _dronePatrol.PointPatrol[index].gameObject.GetComponentInParent<MinePrefabState>().PrefabActive();
     _dronePatrol.PointPatrol[index].gameObject.GetComponentInParent<PipeMoving>().MineActive = true;
 }

 private void OnDisable()
 {
     //_dronePatrol.MineActivate.RemoveListener(Activated);
 }
}


