using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PowerUpButtonControl : MonoBehaviour
{
    [SerializeField] private Text _countHBText,_countRBText,_countSBText, _countABText;
    [SerializeField] private HealthTank _health;
    [SerializeField] private GameObject _hpButton,_rbButton, _sBButton, _aBButton, _armorSphere;
    [SerializeField] private Image _reloadIcon, _speedIcon, _armorIcon;
    [SerializeField] private float _timeToSpawnPU;
    [SerializeField] private Transform[] _spawnPUPosition;
    [SerializeField] private bool levelPU;
    private ObjectPooler _objectPooler;
    private float _timeAcivePU = 5.0f;
    private int  _countHB, _countRB, _countSB, _countAB;
    
    static public bool _reloadBoostsActive = false, _speedBoostsActive = false, _armorBoostsActive = false;

 

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        StartCoroutine(SpawnPowerUp());
    }

    public void AddedHealthBoosts()
    {
         _countHB++;
        CountUpdate();
    }
   
    public void ActiveHealthBoosts()
    {
        _health.ChangeHealth(-30);
         _countHB--;
      
        if ( _countHB <= 0)
        {
            _hpButton.SetActive(false);
        }
        CountUpdate();
    }

    public void AddedReloadBoosts()
    {
        _reloadIcon.fillAmount = 1;
        _countRB++;
        CountUpdate();
    }
   
    public void AddedSpeedBoosts()
    {
        _speedIcon.fillAmount = 1;
        _countSB++;
        CountUpdate();
    }
    public void AddedArmorBoosts()
    {
        _armorIcon.fillAmount = 1;
        _countAB++;
        CountUpdate();
    }
    public void ActiveReloadBoosts()
    {
        _reloadBoostsActive = true;
        _countRB--;
        StartCoroutine(BoostsShootActive(_countRB, _rbButton, _reloadIcon, 1));

        CountUpdate();
    }
    public void ActiveSpeedBoosts()
    {
        _speedBoostsActive = true;
        _countSB--;
        StartCoroutine(BoostsShootActive(_countSB, _sBButton, _speedIcon, 2));

        CountUpdate();
    }
    public void ActiveArmorBoosts()
    {
        _armorBoostsActive = true;
        _armorSphere.SetActive(true);
        _countAB--;
        StartCoroutine(BoostsShootActive(_countAB, _aBButton, _armorIcon, 3));

        CountUpdate();
    }
    private void CountUpdate()
    {
        _countHBText.text = _countHB.ToString();
        _countRBText.text = _countRB.ToString();
        _countSBText.text = _countSB.ToString();
        _countABText.text = _countAB.ToString();
    }
    private IEnumerator BoostsShootActive(int countPower, GameObject ButtonObj, Image iconPw,int numberBoosts)
    {
        while (_timeAcivePU > 0)
        {
            _timeAcivePU -= Time.deltaTime;
            iconPw.fillAmount = _timeAcivePU / 5f;
            yield return null;
        }
        if (countPower <= 0)
        {
            iconPw.fillAmount = 1;
            ButtonObj.SetActive(false);
        
        }
        else
        {
           iconPw.fillAmount = 1;
        }
        _timeAcivePU = 5.0f;
        BoostVariant(numberBoosts);
       
    }

    private void BoostVariant(int value)
    {
        switch (value)
        {
            case 1:
                _reloadBoostsActive = false;
                break;
            case 2:
                _speedBoostsActive = false;
                break;
            case 3:
                _armorBoostsActive = false;
                _armorSphere.SetActive(false);
                break;
           

            default:
                break;
        }
    }

    private IEnumerator SpawnPowerUp()
    {
        while (levelPU)
        {
            yield return new WaitForSeconds(_timeToSpawnPU);
            
            float tempValue = Random.Range(0, 5);
            switch (tempValue)
            {
                case 1:
                    SpawnPU("ArmorPowerUp");
                    break;
                case 2:
                    SpawnPU("ReloadSpeedPowerUp");
                    break;
                case 3:
                    SpawnPU("HealthPowerUp");
                    break;
                case 4:
                    SpawnPU("SpeedPlayerPowerUp");
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnPU(string tag)
    {
        _objectPooler.SpawnFromPoll(tag, _spawnPUPosition[Random.Range(0, _spawnPUPosition.Length)].position, Quaternion.identity);

    }
}
