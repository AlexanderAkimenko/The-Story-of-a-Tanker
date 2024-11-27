using UnityEngine;

[CreateAssetMenu(fileName = "TankConfig", menuName = "Tank Configuration", order = 1)]
public class TankConfig : ScriptableObject
{

    [SerializeField] private string _bulletTag;
    [Range(0.0f, 5.0f)] [SerializeField] private float _reloadingTime;
    [Range(0.0f, 100.0f)] [SerializeField] private float _movementSpeed;
    [Range(0.0f, 100.0f)] [SerializeField] private float _rotationSpeed;


    public string BulletTag => _bulletTag;
    public float ReloadingTime => _reloadingTime;
    public float MovementSpeed => _movementSpeed;

    public float RotationSpeed => _rotationSpeed;
}