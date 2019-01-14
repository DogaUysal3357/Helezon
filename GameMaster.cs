using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    //Coordinator script component
    private Coordinator coordinator;
    private bool tutorialDone = false;

    // Correct button list. 1 is Button1, 2 is Button2, 3 is Button3
    [SerializeField]
    [Range(1, 3)]
    private int[] correctAnswers;

    // Current level index, -1 is tutorial
    private int currentLevel;
    private bool lastLevel;
	private int currMaxLevel;


	[SerializeField]
	private GameObject button_LastLevel;
	[SerializeField]
	private GameObject button_NextLevel;
	[SerializeField]
	private GameObject button_TutorialComplete;
	[SerializeField]
	private GameObject button_LevelComplete;
	[SerializeField]
	private GameObject endLevelScreen;
	[SerializeField]
	private GameObject image_Success;
	[SerializeField]
	private GameObject image_Fail;
    [SerializeField]
    private GameObject buttonToDisable;
	

    void Awake()
    {
        currentLevel = -1;
		currMaxLevel = -1;
        lastLevel = false;
    }


    // Use this for initialization
    void Start()
    {
        coordinator = GameObject.FindGameObjectWithTag("Coordinator").GetComponent<Coordinator>();

		//deactivate button_LastLevel because there's no level to go back to.
		button_LastLevel.SetActive (false);
		button_NextLevel.SetActive (false);
		button_LevelComplete.SetActive (false);
        if (coordinator == null)
        {
            throw new System.Exception("Couldn't find coordinator script component at GameMaster");
        }
    }

    void Update()
    {
        Debug.Log("GameMaster Current Level -> " + currentLevel);

    }

    //TODO: Finishing touch for last level.
    public void IncrementLevel()
    {
        if(!lastLevel) {
            ++currentLevel;
        }
		button_LevelComplete.SetActive(false);

		if (currentLevel > currMaxLevel) {
			++currMaxLevel;
		}else if (currentLevel == currMaxLevel) {
			button_NextLevel.SetActive (false);
		}
			
		if (currentLevel == 1) {
			button_LastLevel.SetActive (true);
		}

		if (lastLevel) {
			button_LevelComplete.SetActive (false);
			button_LastLevel.SetActive (false);
			button_NextLevel.SetActive (false);
            coordinator.LastLevel();
            disableButton();
            endLevelScreen.SetActive (true);
		}
		if (currentLevel == correctAnswers.Length - 1){
            lastLevel = true;
			button_NextLevel.SetActive (false);
        }
    }

	public void DecrementLevel(){
		if (currentLevel != 0) {
			--currentLevel;
			if (currentLevel == 0) {
				button_LastLevel.SetActive (false);		
			}
		}
		if (currentLevel != currMaxLevel) {
			button_NextLevel.SetActive (true);
		}
	}

	public void TutorialDone(){
		tutorialDone = true;
		button_TutorialComplete.SetActive (false);
		IncrementLevel ();
	}

    public int GetCurrentLevelIndex()
    {
        return currentLevel;
    }


	IEnumerator disableSuccess(){
		yield return new WaitForSeconds (0.75f);
		image_Success.SetActive (false);
	}


	IEnumerator disableFail(){
		yield return new WaitForSeconds (0.75f);
		image_Fail.SetActive (false);

	}

    void disableButton() {
        if(buttonToDisable != null) {
            buttonToDisable.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //If any button is pressed, checks if its the correct answer. After that plays the corresponding AudioClip on Coordinator.
    #region ButtonPresses
    public void Button1Pressed()
    {
		if (!tutorialDone) {
            if (!coordinator.isPlaying())
            {
                coordinator.PlayTutorialClip1();
            }
		} else {
			if (correctAnswers [currentLevel] == 1) {
				if (!coordinator.isPlaying ()) {
					coordinator.PlaySuccess ();
					image_Success.SetActive (true);
					StartCoroutine (disableSuccess());
					button_LevelComplete.SetActive (true);
				}
			} else {
                if(!coordinator.isPlaying()){
                    coordinator.PlayFail();
                    image_Fail.SetActive(true);
                    StartCoroutine(disableFail());
                }
			}
		}
    }

    public void Button2Pressed()
    {
        if (!tutorialDone){
            if (!coordinator.isPlaying()){
                coordinator.PlayTutorialClip2();
            }
        }
        else {
			if (correctAnswers [currentLevel] == 2) {
				if (!coordinator.isPlaying ()) {
					coordinator.PlaySuccess ();
					image_Success.SetActive (true);
					StartCoroutine (disableSuccess());
					button_LevelComplete.SetActive (true);
				}
			} else {
				coordinator.PlayFail ();
				image_Fail.SetActive (true);
				StartCoroutine (disableFail());
			}
		}
    }

    public void Button3Pressed()
    {
        if(!tutorialDone){
            if(!coordinator.isPlaying()){
                coordinator.PlayTutorialClip3();
            }
        } else {
			if (correctAnswers [currentLevel] == 3) {
				if (!coordinator.isPlaying ()) {
					coordinator.PlaySuccess ();
					image_Success.SetActive (true);
					StartCoroutine (disableSuccess());
					button_LevelComplete.SetActive (true);
				}
			} else {
				coordinator.PlayFail ();
				image_Fail.SetActive (true);
				StartCoroutine (disableFail());
			}
		}
    }
    #endregion

}
