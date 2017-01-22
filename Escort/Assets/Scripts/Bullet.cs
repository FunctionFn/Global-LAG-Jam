using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    //public ParticleSystem hitPC;


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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Enemy_SlamFish>() || other.gameObject.GetComponent<Enemy_Crab>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
