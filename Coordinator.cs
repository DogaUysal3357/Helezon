using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Coordinator : MonoBehaviour {

    public AudioClip introTutorialSound;
    [Space]
    public AudioClip trueAnswer;
    public AudioClip falseAnswer;
    [Space]
    public AudioClip [] levels;
    
	//TODO: Check tutorial done after finishing script
	private bool tutorialDone = true;
	private GameObject gameMaster;
    private AudioSource source;
	private string sceneName = null;

	private int frameCounter;
	private const int FRAME_COUNTER = 13;
	private const float CLIP_SKIP = 0.1f;
	private int level;
	private bool changeClip = false;


	#region FrameCheckerTemp
	/*
		frameCounter++;

		if(frameCounter >= FRAME_COUNTER) {
			if (timeLeftOnCurrentClip() <= 1) {
				
				frameCounter = 0;
			}
		} 
	 */
	#endregion



	//TODO add Level Name to Exception
	void Awake(){
		frameCounter = 0;
		level = 0;


		//Gets AudioSource in current GameObject(Coordinator)
		source = GetComponent<AudioSource> ();
		if (source == null) {
			throw new System.Exception ("Couldn't find an AudioSource in Coordinator");
		}

	}


	// Use this for initialization
	void Start () {
		//Gets GameMaster object for this script
		gameMaster = GameObject.FindGameObjectWithTag ("GameMaster");
		//Gets current active scene name
		sceneName = SceneManager.GetActiveScene().name;

		//TODO: After writing GameMaster script, get level index from gameMaster

		//FIXME: Remove this 
		UpdateClip();

		if (gameMaster == null) {
			throw new System.Exception ("Couldn't find a GameMaster at Coordinator");
		}
		if (sceneName == null) {
			throw new System.Exception ("Couldn't read scene name at Coordinator");
		}

	}
	
	//TODO: Tutorial clips arent synchronized
	void Update () {

		Debug.Log ("Coordinator Current Level -> " + level);


		if (tutorialDone) {

			//Every 13rd frame, compare Coordinator Current Level and GameMaster Current Level, 
			//if they're different and Coordinator isn't playing anything, get ready to change the current clip
			frameCounter++;
			if(frameCounter >= FRAME_COUNTER) {
				if (level != gameMaster.GetComponent<GameMaster>().GetCurrentLevelIndex() && !source.isPlaying ) {
					changeClip = true;
				}
			frameCounter = 0;
			} 

			//Change current level and update the current clip
			if (changeClip) {
				SetCurrentLevel (gameMaster.GetComponent<GameMaster> ().GetCurrentLevelIndex ());
				UpdateClip ();
				changeClip = false;
			}

		} else {
			Tutorial ();
		}
	}

	void OnMouseDown(){
		//If coordiantor isn't speaking at the moment and coordinator is clicked, PlayCurrentClip
		if (!source.isPlaying) {
			PlayCurrentClip ();
		}
	}

	private void Tutorial(){
		frameCounter++;

		if (source.clip == introTutorialSound) {
			if(frameCounter >= FRAME_COUNTER) {
				if (timeLeftOnCurrentClip() <= 1) {
					tutorialDone = true;
					frameCounter = 0;
				}
			} 
		}
	}
		

	//Returns the remaining time in seconds on current audioclip.
	private float timeLeftOnCurrentClip() {
		float timeLeft = 0;
		timeLeft = source.clip.length - source.time;
		return timeLeft;
	}

	//Plays the intro clip.
	private void PlayIntro(){
		source.clip = introTutorialSound;
		source.Play ();
	}

	// Plays the Succesfull Answer clip
	public void PlaySuccess(){
		if (!source.isPlaying) {
			source.clip = trueAnswer;
			source.Play ();
		}
	}

	// Plays the Wrong Answer clip
	public void PlayFail(){
		if (!source.isPlaying) {
			source.clip = falseAnswer;
			source.Play ();
		}
	}

	//Plays the clip for current level
	private void PlayCurrentClip(){
		source.Play ();
	}

	//Plays the clip for level X 
	public void PlayClipForLevelX(int i){
		source.clip = levels [i - 1];
		source.Play ();
	}

	//Sets current level
	public void SetCurrentLevel(int i){
		level = i;
	}
		
	private void UpdateClip(){
		source.clip = levels [level];
	}
}
