using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum LEVEL_NAMES
{
	Room1,
	Room2

}

public class GameManager : MonoBehaviour 
{

	public LEVEL_NAMES NextLevelName;


	public void LoadNextScene()
	{
		if (NextLevelName == LEVEL_NAMES.Room1) 
			SceneManager.LoadScene ("Level1");

	}
    public void QuitGame()
    {
        Application.Quit();
    }


}