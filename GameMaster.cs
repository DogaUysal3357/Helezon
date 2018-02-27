using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	//Coordinator script component
	private Coordinator coordinator;

	// Correct button list. 1 is Button1, 2 is Button2, 3 is Button3
	[SerializeField][Range(1,3)]
	private int[] correctAnswers; 

	// Current level index, -1 is tutorial
	private int currentLevel;	
	private bool lastLevel;



	void Awake(){
		currentLevel = 0;
		lastLevel = false;
	}


	// Use this for initialization
	void Start () {
		coordinator = GameObject.FindGameObjectWithTag ("Coordinator").GetComponent<Coordinator> ();


		if (coordinator == null) {
			throw new System.Exception ("Couldn't find coordinator script component at GameMaster");
		}
	}

	void Update(){
		Debug.Log ("GameMaster Current Level -> " + currentLevel);
	
	}
		
	//TODO: Finishing touch for last level.
	public void IncrementLevel(){
		currentLevel++;
		if (currentLevel == correctAnswers.Length - 1) {
			lastLevel = true;
		}
	}

	public int GetCurrentLevelIndex(){
		return currentLevel;
	}

	//If any button is pressed, checks if its the correct answer. After that plays the corresponding AudioClip on Coordinator.
	#region ButtonPresses
	public void Button1Pressed(){
		if (correctAnswers [currentLevel] == 1) {
			coordinator.PlaySuccess();
			IncrementLevel ();
		} else {
			coordinator.PlayFail ();
		}
	}
		
	public void Button2Pressed(){
		if (correctAnswers [currentLevel] == 2) {
			coordinator.PlaySuccess();
			IncrementLevel ();
		} else {
			coordinator.PlayFail ();
		}
	}
		
	public void Button3Pressed(){
		if (correctAnswers [currentLevel] == 3) {
			coordinator.PlaySuccess();
			IncrementLevel ();
		} else {
			coordinator.PlayFail ();
		}
	}
	#endregion

}
