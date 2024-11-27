using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public static UnityEvent OnStartShooting = new UnityEvent();
    public static UnityEvent OnStopShooting = new UnityEvent();
    public static UnityEvent PlayerOnShoot = new UnityEvent();

}
