using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemsyAgent : BaseTank
{
    
    [SerializeField] private Transform[] _pointsToMove;
    [SerializeField] private float _timeToWait = 5.0f, _distanceToPlayer = 5.0f;
    [SerializeField] private bool _playerDetection;
    [SerializeField] private GameObject _player;

    private NavMeshAgent _agent;
    private Transform _currentPos;
    private float _time;

    public bool _lookToPlayer = false;
    


    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _time = _timeToWait;
        SetNewPos();

    }

    private void Update()
    {
        if (_playerDetection)
        {
            SetNewPos();
        }
        else
        {
            StartCoroutine(AgentWait());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _playerDetection = true;
        }

      
        
    }
    private IEnumerator AgentWait()
    {
        if (_time >= 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            SetNewPos();
            _time = _timeToWait;
        }

        yield return new WaitForSeconds(_timeToWait);
    }
    private void SetNewPos()
    {
        if (_playerDetection && _player !=null)
        {
         
            if ((Vector3.Distance(transform.position,_player.transform.position) > _distanceToPlayer))
            {  
                _agent.SetDestination(_player.transform.position);
                _lookToPlayer = false;
            }
            else
            {
                _agent.isStopped = true;
                _agent.ResetPath();
                _agent.transform.LookAt(_player.transform.position);
                _lookToPlayer = true;
            }

        }
        else
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

    }
}

