using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] EnemyPatrol _enemyPatrol;
    [SerializeField] private NavMeshAgent _agent;
    private int _objnumber;
    private string _nameLayer = "EnemyDetector";
    private ObjectNumber  _currentNumber;

    private void Start()
    {
        _currentNumber = gameObject.GetComponentInParent<ObjectNumber>();
        _objnumber = _currentNumber != null ? _currentNumber.Number:0;
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_currentNumber != null)
        {
            if (_nameLayer == LayerMask.LayerToName(other.gameObject.layer))
            {
                int numb = other.GetComponentInParent<ObjectNumber>().Number;
          
            
                    if (_objnumber > numb)
                    {
                        return;
                    }
                    else if (_objnumber < numb)
                    {
                        _agent.isStopped = true;
                    }
                    else if (_objnumber == numb)
                    {
                        EnemyPatrol collisionEnemy = other.GetComponentInParent<EnemyPatrol>();
                        if (_enemyPatrol.NumberInHierarchy > collisionEnemy.NumberInHierarchy)
                        {
                            return;
                        }
                        else
                        {
                            _agent.isStopped = true;
                        }
                    }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_nameLayer == LayerMask.LayerToName( other.gameObject.layer))
        {
            _agent.isStopped = false;
        }
    }
}
