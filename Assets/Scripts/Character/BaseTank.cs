using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseTank : MonoBehaviour
{ 
    protected ObjectPooler _objectPooler;
    protected Rigidbody _rigidbody;
    protected TankConfig _tankConfig;
    protected LevelControll _levelControll;
    public EnemyTypes EnemyType;

    protected virtual void  Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

   protected virtual void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _tankConfig = GetComponent<LinkSOTank>().TankConfig;
        _levelControll = Camera.main.GetComponent<LevelControll>();
    }
}
public enum EnemyTypes
{
    LightTank,
    MediumTank,
    HeavyTank,
    PT_SAU,
    MouseBoss,
    //
    Player
}

