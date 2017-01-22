using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    //public ParticleSystem hitPC;
    void Start()
    {
        Physics.IgnoreLayerCollision(10, gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 2);
    }

    void OnDisable()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy_SlamFish>() || other.gameObject.GetComponent<Enemy_Crab>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
