using System.Collections;
using UnityEngine;

public class BossPreview : MonoBehaviour
{

    public Transform Target,FallTarget, BalconyFT;

    public GameObject Balcony, BossAgent,ParticleExplosion;
    public float Speed = 1.0f;
    private bool _moveBalcony,_startPreview;

    void Update()
    {
        if (_startPreview)
        {
            if (_moveBalcony)
            {
                Balcony.transform.position = Vector3.MoveTowards(Balcony.transform.position, BalconyFT.position, Speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, FallTarget.position, Speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Движение босса");
                transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossPoint"))
        {
            GetComponent<ActiveCameraShake>().TriggerShakeBoss();
            _moveBalcony = true;
            StartCoroutine(StartFight());
            Handheld.Vibrate();
        }
    }
    public void StartPreview()
    {
        _startPreview = true;
    }
    IEnumerator StartFight()
    {
        yield  return new WaitForSeconds(3);
        Destroy(Balcony);
        BossAgent.transform.position = transform.position;
        BossAgent.transform.rotation = transform.rotation;
        ParticleExplosion.transform.position = transform.position;
        ParticleExplosion.SetActive(true);
        BossAgent.SetActive(true);
        Destroy(gameObject);
        Handheld.Vibrate();
    }
  
}
