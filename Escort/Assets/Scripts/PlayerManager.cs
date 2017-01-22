using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    //Instantiation
    private static PlayerManager _inst;
    public static PlayerManager Inst { get { return _inst; } }


    public PlayerController[] Players;

    void Awake()
    {
        _inst = this;
    }

    // Update is called once per frame
    void Update () {
	    
	}

    public int GetWinners()
    {
        float max = Players[0].grabTime;
        int maxp = 0;

        for (int i = 0; i < 4; ++i)
        {
            if(Players[i].grabTime > max)
            {
                max = Players[i].grabTime;
                maxp = i;
            }
        }

        return maxp;
    }
}
