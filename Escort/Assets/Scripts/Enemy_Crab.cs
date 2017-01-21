using UnityEngine;
using System.Collections;

public class Enemy_Crab : MonoBehaviour {

    public GameObject target;

    public float speed = 0.025f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move towards target
        Vector3 targetDir = target.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, speed, 0.0F));
    }
}
