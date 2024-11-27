using UnityEngine;


public class LevelControll : MonoBehaviour
{
    [SerializeField] private GameObject _nextLevelButton;
    [Header("Max spawn")]
    [SerializeField]  private int _maxLightTank, _maxMediumTank, _maxHeavyTank, _maxPTSay, _maxARTSay;

    [Header("Current counts tanks on Scene")]
    [SerializeField]  private int _currentLT, _currentMT, _currentHT,_currentPT, _currentSAY;
    [SerializeField] private Transform[] _spawnPos;
    [SerializeField] private BossPreview _bossPreview;
    private ObjectPooler _objectPooler;
     int _waveNumber = 0;

    [Header("waves before the boss fight")]
    [SerializeField] private int _countWave;
 


    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void LevelProgress (EnemyTypes type)
    {
        _waveNumber++;
       switch(type)
        {
            
            case EnemyTypes.LightTank:
                Debug.Log("SpawnLight");
                if (_currentLT < _maxLightTank)
                {
                    _currentLT++;
                    _objectPooler.SpawnFromPoll("LightTank", _spawnPos[Random.Range(0, _spawnPos.Length)].position, Quaternion.identity);
                }
              
                break;

            case EnemyTypes.MediumTank:
                Debug.Log("SpawnMedium");
                break;

            case EnemyTypes.HeavyTank:
                Debug.Log("SpawnHeavy");
                break;

            case EnemyTypes.PT_SAU:
                Debug.Log("SpawnPT");
                if (_currentPT < _maxPTSay)
                {
                    _currentPT++;
                    _objectPooler.SpawnFromPoll("Stug3", _spawnPos[Random.Range(0, _spawnPos.Length)].position, Quaternion.identity);
                }
             
                break;

            case EnemyTypes.MouseBoss:
                Debug.Log("win");
                _nextLevelButton.SetActive(true);
                break;

            case EnemyTypes.Player:
                Debug.Log("GameOver");
                break;

            default:
                Debug.LogError("ErrorType");
                break;
        }


      

        if (_waveNumber >= _countWave )
        {
            Debug.Log("StartBoss");
            _bossPreview.StartPreview();
        }

    }

   public int GetWaveNumber()
    {
        return  _waveNumber;
    }

}
