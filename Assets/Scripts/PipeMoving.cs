using UnityEngine;
using UnityEngine.Events;

public class PipeMoving : MonoBehaviour
{
    [SerializeField] private MinePrefabState _minePrefabState;
    [SerializeField] private float _speed = 2.0f; 
    [SerializeField] private float _moveDistance = 5.0f;

    private Vector3 _startPosition;
    private bool _movingDown = true;
    private GameObject _mineObj;
    
    [HideInInspector] public bool MineActive = false;
    
    public UnityEvent  MineExplosion = new UnityEvent ();
    void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        if (MineActive)
        {
            if (_movingDown)
            {
                transform.position -= new Vector3(0, _speed * Time.deltaTime, 0);

                if (Vector3.Distance(_startPosition, transform.position) >= _moveDistance)
                {
                    _movingDown = false;
                    MineExplosion?.Invoke();
                }
            }
            else
            {
                transform.position += new Vector3(0, _speed * Time.deltaTime, 0);

                if (Vector3.Distance(_startPosition, transform.position) <= 0.1f)
                {
                    MineActive = false;
                    _movingDown = true;
                }
            }
        }
    }

  

  
}
