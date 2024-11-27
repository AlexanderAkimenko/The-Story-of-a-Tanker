using UnityEngine;

public class RainActive : MonoBehaviour
{
    [SerializeField] GameObject _rain;
    private LevelControll _levelControll;
    

    private void Start()
    {
        _levelControll = GetComponent<LevelControll>();
    }
    private void Update()
    {
        if (_levelControll.GetWaveNumber() == 10)
        {
            _rain.SetActive(true);
        }
    }
}
