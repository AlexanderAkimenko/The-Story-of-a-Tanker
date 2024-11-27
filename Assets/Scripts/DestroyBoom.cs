using UnityEngine;

public class DestroyBoom : MonoBehaviour
{
   [SerializeField] private float _lifeTime = 5.0f;

    private void OnEnable()
    {
        _lifeTime = 5.0f;
    }
    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <=0)
        {
            gameObject.SetActive(false);
        }

    }
}
