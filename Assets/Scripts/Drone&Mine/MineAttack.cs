using UnityEngine;

public class MineAttack : MonoBehaviour
{
    [SerializeField] private float  _explosionRadiusMax = 85.0f;
    [SerializeField] private float _mineDamage = 25.0f;
    [SerializeField] private PipeMoving _pipeMoving;
    
    private GameObject _player;
        
    private float _distanceToPlayer;



    private void OnEnable()
    {
     
    
        _pipeMoving.MineExplosion.AddListener(AttackMine);
        PlayerMovement _playerMovement = FindObjectOfType<PlayerMovement>();
        if (_playerMovement !=null)
        {
            _player = _playerMovement.gameObject;
        }  
    }

   
    private void Update()
    {
        if (_player !=null)
        {
            CheckDistanceToPlayer();
        }
  
    }

    private void CheckDistanceToPlayer()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log("Distance To Player = " + _distanceToPlayer);
    }

    private void AttackMine()
    {
        if (_player != null)
        {
            if (_distanceToPlayer < _explosionRadiusMax)
            {
               _player.GetComponent<HealthTank>().ChangeHealth(20);
            }
        }
    }

    private void OnDisable()
    {
        _pipeMoving.MineExplosion.RemoveListener(AttackMine);
    }
}
