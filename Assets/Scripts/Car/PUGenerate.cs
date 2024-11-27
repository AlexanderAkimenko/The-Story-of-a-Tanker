
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BoxPUType
{
    Health,
    Armor,
    Reload,
    Speed
}
public  class PUGenerate : MonoBehaviour
{
    [SerializeField] private CarTrigger _carTrigger;
    [SerializeField] private GameObject[] _boxModel;
    private BoxPUType _boxType;
    private int _randomValue;
    private ObjectPooler _objectPooler;
    private void Start()
    {
    _objectPooler = ObjectPooler.Instance;
    DeactiveBox();
    ActivePUBox();
    }

    private void OnEnable()
    {
        _carTrigger.KilledByAPlayer += ActivePUPrefab;
     _carTrigger.CarDestroy += DeactiveBox;
        _carTrigger.CarDestroy +=ActivePUBox;
  

    }

    private void OnDisable()
    {
        _carTrigger.KilledByAPlayer -= ActivePUPrefab;
        _carTrigger.CarDestroy -= DeactiveBox;
        _carTrigger.CarDestroy -=ActivePUBox;
    }

    private void ActivePUBox()
    {
        switch (_randomValue)
        {
            case 0:
            {
                _boxModel[0].SetActive(true);
                _boxType = BoxPUType.Health;
            }
                break;
            case 1:
            {
                _boxModel[1].SetActive(true);
                _boxType = BoxPUType.Armor;
            }
                break;
            case 2:
            {
                _boxModel[2].SetActive(true);
                _boxType = BoxPUType.Speed;
            }
                break;
            case 3:
            {
                _boxModel[3].SetActive(true);
                _boxType = BoxPUType.Reload;
            }
                break;
            default: 
                Debug.LogError("Error");
                break;
        }
    }

    private void DeactiveBox()
    {
        _randomValue = Random.Range(0, 4); 
        foreach (var val in _boxModel)
        {
            val.SetActive(false);
        }
    }

    private void ActivePUPrefab()
    {
        switch (_boxType)
        {
            case BoxPUType.Armor:
                SpawnPU("ArmorPowerUp");
                break;
            case BoxPUType.Reload:
                SpawnPU("ReloadSpeedPowerUp");
                break;
            case BoxPUType.Health:
                SpawnPU("HealthPowerUp");
                break;
            case BoxPUType.Speed:
                SpawnPU("SpeedPlayerPowerUp");
                break;
            default:
                Debug.LogError("error");
                break;
        }
    }

    private void SpawnPU(string tag )
    {
        _objectPooler.SpawnFromPoll(tag, _carTrigger.gameObject.transform.position, Quaternion.identity);
    }
}
