using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PowerUpSoundManager _pUSoundManager;
    [SerializeField] private GameObject _hBButton,_rBButton,_sBButton, _aBButton;
    [SerializeField] private PowerUpButtonControl _powerUpBC;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            _pUSoundManager.AddedPU?.Invoke();
            
            switch (other.gameObject.GetComponent<PowerUp>().PowerUpType)
            {
                
                case PowerUp.PowerUpTypes.HealthBoosts:
                    if (_hBButton.activeSelf == false)
                    {
                      _powerUpBC.AddedHealthBoosts();
                        _hBButton.SetActive(true);
                    }
                    else
                    {
                        _powerUpBC.AddedHealthBoosts();
                    }
                  
                    break;
                case PowerUp.PowerUpTypes.ReloadBoosts:
                    if (_rBButton.activeSelf == false)
                    {
                        _powerUpBC.AddedReloadBoosts();
                        _rBButton.SetActive(true);
                    }
                    else
                    {
                        _powerUpBC.AddedReloadBoosts();
                    }

                    break;

                case PowerUp.PowerUpTypes.PlayerSpeedBoosts:
                    if (_sBButton.activeSelf == false)
                    {

                        _powerUpBC.AddedSpeedBoosts();
                        _sBButton.SetActive(true);
                    }
                    else
                    {
                        _powerUpBC.AddedSpeedBoosts();
                    }
                    break;

                case PowerUp.PowerUpTypes.ArmorActive:
                    if (_aBButton.activeSelf == false)
                    {

                        _powerUpBC.AddedArmorBoosts();
                        _aBButton.SetActive(true);
                    }
                    else
                    {
                        _powerUpBC.AddedArmorBoosts();
                    }
                    break;
                default:
                    break;
            }
            other.gameObject.SetActive(false);
        }
    }
}
