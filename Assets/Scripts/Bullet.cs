using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private string _tankTag;
    [Range(50.0f, 150.0f)]
    [SerializeField] private float _speed = 5.0f;
    

    private float _activeTime = 5.0f;

    private void Update()
    {
        BulletActive();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }
    private void BulletActive()
    {
      
        if (gameObject.activeSelf)
        {

            if (_activeTime > 0)
            {
                _activeTime -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                 _activeTime = 5.0f;
            }
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != _tankTag)
        {
            HealthTank health = other.GetComponent<HealthTank>();
            
            if (health != null)
            {
                health.ChangeHealth(_damage);
                gameObject.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("Envirmoment"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Armor"))
        {
            if (_tankTag == "Mouse")
            {
                return;
            }
            else
            {
                 gameObject.SetActive(false);
            }
        }

    }

    public string GetTankTag() => _tankTag;
}
