using UnityEngine;
using System.Collections;

public class Attack_EnemyWave : MonoBehaviour {

    public int waveMaxTime = 50;

    public int waveTimer = 0;

    private bool activeWave = false;
  

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
}
