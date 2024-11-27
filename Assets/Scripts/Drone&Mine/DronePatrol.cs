using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;



public class DronePatrol : MonoBehaviour
{ 
    public List<Transform> PointPatrol;
   
    [SerializeField] private AnimationCurve _heightCurve;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private int _timePipeDeactive = 15, _stepToActiveMine = 5;
    
    [HideInInspector] public UnityEvent <int> MineActivate = new UnityEvent <int>();
    
    private int _nextIndex;
    private int _currentMoveIndex;
    private int _pointsPassed;

    private float _distanceBetweenPoints; // Расстояние между текущей и следующей точкой
    private float _journeyLength; // Общая длина перемещения между текущей и следующей точкой
    private float _startTime; 
    private float distCovered;
    
    private Vector3 _startPosition; 
    private Vector3 _endPosition;
    
    private bool _firstMove = true;
  
    private List<bool> _pointAvailable; 
    
    private void Start()
    {
        InitializePointAvailability();
        SetNextMovePoint();
        MineActivate.AddListener(ResetPassed);
    }
    void Update()
    {
       
        
         distCovered = (Time.time - _startTime) * _speed / _journeyLength;
        
        
        transform.position = Vector3.Lerp(_startPosition, _endPosition, distCovered);
        transform.position = new Vector3(transform.position.x, (transform.position.y + _heightCurve.Evaluate(distCovered) * 50.0f), transform.position.z);


        if (distCovered >= 1.0f)
        {
            _pointsPassed++;
           
            if (_pointsPassed >=_stepToActiveMine)
            {
                MineActivate?.Invoke(_currentMoveIndex);
                
            }
            SetNextMovePoint();
        }

        
    }
    private void SetNextMovePoint()
    {
        do
        {
            _nextIndex = Random.Range(0, PointPatrol.Count);
           
            if (_firstMove )
            {
                _firstMove = false;
                _currentMoveIndex = _nextIndex;
            }
        } while (_nextIndex == _currentMoveIndex || !_pointAvailable[_nextIndex]);
     
        _distanceBetweenPoints =  Vector3.Distance(PointPatrol[_currentMoveIndex].position, PointPatrol[_nextIndex].position);
        _journeyLength = _distanceBetweenPoints ;
        _startTime = Time.time;
        _startPosition = PointPatrol[_currentMoveIndex].position;
        _endPosition = PointPatrol[_nextIndex].position;
        _currentMoveIndex = _nextIndex;

      

    }

    private void ResetPassed( int a)
    {
        StartCoroutine( MakePointAvailableAfterDelay(_currentMoveIndex));
        _pointsPassed = 0;
    }

    private void InitializePointAvailability()
    {
        _pointAvailable = new List<bool>(PointPatrol.Count);
        for (int i = 0; i < PointPatrol.Count; i++)
        {
            _pointAvailable.Add(true);
        }
    }
    private IEnumerator MakePointAvailableAfterDelay(int pointIndex)
    {
        _pointAvailable[pointIndex] = false;

        yield return new WaitForSeconds(_timePipeDeactive);
        _pointAvailable[pointIndex] = true;

    }
}
