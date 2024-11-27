using UnityEngine;

public class TowardControll : MonoBehaviour
{
    private Vector3 _rotationToPlayer;

    [SerializeField] private EnemyPatrol _agent;
    [SerializeField] private float _rotationSpeed;
    void Update()
    {
        if (EnemyPatrol._playerDetection && _agent._player != null)
        {
            _rotationToPlayer = _agent._player.transform.position - transform.position;
            _rotationToPlayer.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(_rotationToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    
    }
}
