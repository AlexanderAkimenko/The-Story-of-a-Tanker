using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyShootable))]
[RequireComponent(typeof(HealthTank))]
[RequireComponent(typeof(LinkSOTank))]
public class EnemyPatrol : BaseTank
{
    [SerializeField] private Transform[] _pointsToMove;
    [SerializeField] private float _timeToWait = 5.0f, _distanceToPlayer = 5.0f;
    [SerializeField] private bool _isABoss;
    [HideInInspector] public GameObject _player;
    [SerializeField] private HealthTank _healthTank;

    static public bool _playerDetection;

    
    private Transform _currentPos;
    private NavMeshAgent _agent;
    private Vector3 _rotationToPlayer;
    private float _currentWaitTime;

    public bool IsALookToPlayer = false;
    public int NumberInHierarchy= 0;


    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _tankConfig.MovementSpeed;
        FindPlayer();
        SetNewPos();
        _currentWaitTime = _timeToWait;
    }
   
    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && !_isABoss)
        {

            _currentWaitTime -= Time.deltaTime;
            if (_currentWaitTime <= 0)
            {
                SetAPreparedPosition();
                _currentWaitTime = _timeToWait;
            }
        }
        SetNewPos();
       
    }

    private void SetNewPos()
    {
        switch (EnemyType)
        {
            case EnemyTypes.LightTank:
                    _currentWaitTime -= Time.deltaTime;
                    if (_currentWaitTime <= 0)
                    {
                        SetAPreparedPosition();
                        _currentWaitTime = _timeToWait;
                    }
                break;

            case EnemyTypes.MediumTank:
                _playerDetection = true;
                Debug.Log(_healthTank.GetHPInProcent() + "HP140OBJ");
                if (_healthTank.GetHPInProcent() >= 50.0f)
                {
                    if ((Vector3.Distance(transform.position, _player.transform.position) > _distanceToPlayer))
                    {
                        _agent.SetDestination(_player.transform.position);
                    } 
                }
                else
                {
                    _currentWaitTime -= Time.deltaTime;
                    if (_currentWaitTime <= 0)
                    {
                        SetAPreparedPosition();
                        _currentWaitTime = _timeToWait;
                    }
                }
              
                break;

            case EnemyTypes.HeavyTank:
                if (_playerDetection)
                {
                    if ((Vector3.Distance(transform.position, _player.transform.position) > _distanceToPlayer))
                    {
                        _agent.SetDestination(_player.transform.position);
                    }
                }
                break;

            case EnemyTypes.PT_SAU:
                if (_playerDetection && _player != null)
                {

                    if ((Vector3.Distance(transform.position, _player.transform.position) > _distanceToPlayer))
                    {
                        _agent.SetDestination(_player.transform.position);
                        IsALookToPlayer = false;
                    }
                    else
                    {
                        LookToPlayer();
                    }



                }
                else SetAPreparedPosition();

                break;

         
            case EnemyTypes.MouseBoss:

                _playerDetection = true;
                if ((Vector3.Distance(transform.position, _player.transform.position) > _distanceToPlayer))
                {
                    _agent.SetDestination(_player.transform.position);
                }

                break;
            default:
                Debug.LogError("ErrorType");
                break;
        }

      
    }
    private void SetAPreparedPosition()
    {
        int randomValue = Random.Range(0, _pointsToMove.Length);
        Transform moveTo = _pointsToMove[randomValue];
        if (moveTo != _currentPos)
        {
            _currentPos = moveTo;
            _agent.SetDestination(_currentPos.position);
        }
        else
        {
            SetNewPos();
        }
    }
   
    private void LookToPlayer()
    {
        IsALookToPlayer = true;
        _agent.isStopped = true;
        _agent.ResetPath();
        _rotationToPlayer = _player.transform.position - transform.position;
        _rotationToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(_rotationToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _tankConfig.RotationSpeed);
    }
    private void FindPlayer()
    {
        _player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _playerDetection = true;
        }

    }
}
