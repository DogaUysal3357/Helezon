using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coordinator : MonoBehaviour {



    public AudioClip introTutorialSound;
    [Space]
    public AudioClip trueAnswer;
    public AudioClip falseAnswer;
    [Space]
    public AudioClip [] levels;
    

	private bool tutorialDone = false;
	private GameObject gameMaster;
    private AudioSource source;
	private string sceneName = null;

	private int frameCounter;
	private const int FRAME_COUNTER = 13;
	private int level;









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


		if (gameMaster == null) {
			throw new System.Exception ("Couldn't find a GameMaster at Coordinator");
		}
		if (sceneName == null) {
			throw new System.Exception ("Couldn't read scene name at Coordinator");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (tutorialDone) {

			//TODO: Add main level clips.

		} else {
			Tutorial ();
		}
	}

	/*
		frameCounter++;

		if(frameCounter >= FRAME_COUNTER) {
			if (timeLeftOnCurrentClip() <= 1) {
				
				frameCounter = 0;
			}
		} 
	 */

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
		source.clip = trueAnswer;
		source.Play ();
	}

	// Plays the Wrong Answer clip
	public void PlayFail(){
		source.clip = falseAnswer;
		source.Play ();
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

}
