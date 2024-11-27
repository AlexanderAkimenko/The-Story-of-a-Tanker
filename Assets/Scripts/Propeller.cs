
using UnityEngine;

public class Propeller : MonoBehaviour
{
 [Range(0.0f,100.0f)]
 [SerializeField] private float _rotationSpeed = 50.0f;

 private void Update()
 {
  transform.Rotate(0,_rotationSpeed *50.0f * Time.deltaTime,0);
 }
}
