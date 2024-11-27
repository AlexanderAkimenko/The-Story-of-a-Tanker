

using UnityEngine;

public class DroneActivation : MonoBehaviour
{
  [SerializeField] private HealthTank _red, _blue;
  [SerializeField] private GameObject _drone;

  private bool _droneActive;
  private void Update()
  {
    if ( _red.GetHPInProcent() + _blue.GetHPInProcent() <= 100 && !_droneActive)
    {
      _drone.SetActive(true);
      _droneActive = true;
    }

   
  }
}
