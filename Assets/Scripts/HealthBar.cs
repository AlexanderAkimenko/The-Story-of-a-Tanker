
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthTank _healthTank;
    [SerializeField] private Image _baseTankBarFilling;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private bool _thisAPlayer = false, _thisABoss;
    
    private Camera _camera;


    private void Awake()
    {
        _camera = Camera.main; 
    }
    private void OnEnable()
    {
        _baseTankBarFilling.color = _gradient.Evaluate(1);
        _healthTank.HealthChanged += OnHealthChanged;
    }
    private void OnDisable()
    {
        _healthTank.HealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float valueAsPercantage)
    {
        _baseTankBarFilling.fillAmount = valueAsPercantage;
        _baseTankBarFilling.color = _gradient.Evaluate(valueAsPercantage);
    }
    private void LateUpdate()
    {
        if (!_thisAPlayer && !_thisABoss)
        {
            transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
            transform.Rotate(0, 180, 0);
        }
       
    }
}
