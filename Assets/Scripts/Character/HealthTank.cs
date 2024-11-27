using System;
using UnityEngine;


public  class HealthTank : BaseTank
{
    [Header("Base option")]
    [SerializeField] protected int _maxHealth;
    [SerializeField] private GameObject  _TheEndBackGround;
    
    protected int _currentHealth;
    private EnemyPatrol _enemyPatrol;

    public event Action <float> HealthChanged;


    private void OnEnable() => _currentHealth = _maxHealth;

    protected override void Start()
    {
        base.Start();
        _objectPooler = ObjectPooler.Instance;
        _enemyPatrol = GetComponent<EnemyPatrol>();
        if (EnemyType == EnemyTypes.Player)
        {
            Time.timeScale = 1;
        }
     
    }

    public void ChangeHealth(int value)
    {
        CheckAgentActive();
        if (EnemyType == EnemyTypes.Player && PowerUpButtonControl._armorBoostsActive)
        {
            return;
        }
        else
        {
           _currentHealth -= value;
        }

        if (_currentHealth <= 0)
        {
            if (EnemyType == EnemyTypes.Player)
            {
                gameObject.SetActive(false);
                Time.timeScale = 0;
                _TheEndBackGround.SetActive(true);
            }
            else
            {
                _objectPooler.SpawnFromPoll("BoomVfx", transform.position + Vector3.up, Quaternion.identity);
            }
            if (_levelControll !=null)
            {
                _levelControll.LevelProgress(EnemyType);
            }
            gameObject.SetActive(false);
        }
            float _currentHealthAsPercantage = (float)_currentHealth / _maxHealth;
            HealthChanged?.Invoke(_currentHealthAsPercantage);
    }



    public float GetHPInProcent()
    {
       return  _currentHealth * 100 / _maxHealth;
    }
    private void CheckAgentActive()
    {
        if (_enemyPatrol != null  && !_enemyPatrol.enabled)
        {
            _enemyPatrol.enabled = true;
        }
    }
}
