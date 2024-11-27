using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f, _minDistance = 0.6f;
    private GameObject _player;
    private bool _onPlayer;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerMovement>()?.gameObject;
       
    }
    private void FixedUpdate()
    {
        if (!_onPlayer && _player != null)
        {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * 100 * Time.deltaTime);
        rb.MovePosition(newPosition);
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < _minDistance)
        {
            GetComponentInChildren<FallRocket>().TriggerFall();
            _onPlayer = true;

        }

}
}
