using UnityEngine;

public class ShadowTrigger : MonoBehaviour
{
   public void ActiveTrigger()
    {
        ActiveShadowRocket.RocketActive = true;
     
        Debug.Log("RocketFall");

    }
}
