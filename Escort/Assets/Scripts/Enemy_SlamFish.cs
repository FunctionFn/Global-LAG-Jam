using UnityEngine;
using System.Collections;

public class Enemy_SlamFish : MonoBehaviour {

    private int jumpTimer;
    private int jumpCount;

    public float jumpHeight;

    // Use this for initialization
    void Start ()
    {
        // Reset timer
        jumpTimer = 0;
        jumpCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Countdown until jump
        jumpTimer++;

        if (jumpTimer >= 200)
        {
            // Check if Slamming
            if (jumpCount >= 2)
            {
                Slam();
                jumpCount = -1;
            }
            else
            {
                Jump();
            }

            jumpTimer = 0;
            jumpCount++;
        }


    }

    void Jump()
    {
        int direction = Random.Range(0, 4);

        // Jump!
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);

        // Jump in a random direction
        if (direction == 0)
            GetComponent<Rigidbody>().AddForce(Vector3.forward * jumpHeight);
        else if (direction == 1)
            GetComponent<Rigidbody>().AddForce(Vector3.back * jumpHeight);
        else if (direction == 2)
            GetComponent<Rigidbody>().AddForce(Vector3.right * jumpHeight);
        else if (direction == 3)
            GetComponent<Rigidbody>().AddForce(Vector3.left * jumpHeight);

    }

    void Slam()
    {
        // Slam!
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight * 3);
    }
}
