using System.Collections;
using UnityEngine;

public class InstallStartPos : MonoBehaviour
{
   
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
    }
    private void OnDisable()
    {
        transform.position = _startPos;
  
    }
 
}
