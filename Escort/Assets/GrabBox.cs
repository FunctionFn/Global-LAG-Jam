using UnityEngine;
using System.Collections;

public class GrabBox : MonoBehaviour {

    public PlayerController player;

    public bool active;


	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(active && other.gameObject.GetComponent<Pickupable>())
        {
            player.Grab(other.gameObject.GetComponent<Pickupable>());
        }
    }
}
