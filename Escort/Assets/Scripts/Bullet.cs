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
}
