using UnityEngine;
using System.Collections;

public class Attack_EnemyBullet : MonoBehaviour
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

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerController>().Damage(1);
            Destroy(gameObject);
        }

        
    }
}
