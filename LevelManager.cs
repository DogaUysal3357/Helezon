using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {


	
	public void LoadMainMenu() {
		SceneManager.LoadScene (0);
	}

	public void LoadLevelAt(int i){
		if (i < 0) {
			Debug.Log ("Error at LevelManager. Given level index " + i);
		}
		SceneManager.LoadScene (i);
	}

    public void QuitProgram() {
        Application.Quit();
    }

}

