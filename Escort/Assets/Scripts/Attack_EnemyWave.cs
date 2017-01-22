using UnityEngine;
using System.Collections;

public class Attack_EnemyWave : MonoBehaviour {

    public int waveMaxTime = 50;

    public int waveTimer = 0;

    public int contactDamage = 1;

	// Use this for initialization
	void Start ()
    {
        StartWave();
	}
	
	// Update is called once per frame
	void Update ()
    {
        waveTimer++;

        // Is wave done?
        if (waveTimer >= waveMaxTime)
        {
            Destroy(gameObject);
        }
	}

    public void StartWave (/*Vector3 pos*/)
    {
        waveTimer = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            other.gameObject.GetComponent<PlayerController>().Damage(contactDamage);
            Destroy(gameObject);
        }
    }
}
