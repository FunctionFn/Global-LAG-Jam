using UnityEngine;
using System.Collections;

public class Attack_EnemyBullet : MonoBehaviour
{

    //public ParticleSystem hitPC;
    public int dmg;

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
            col.gameObject.GetComponent<PlayerController>().Damage(dmg);
            Destroy(gameObject);
        }

        
    }
}
