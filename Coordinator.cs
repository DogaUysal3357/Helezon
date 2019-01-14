using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Coordinator : MonoBehaviour
{

    public AudioClip introTutorialSound;
    [Space]
    public AudioClip trueAnswer;
    public AudioClip falseAnswer;
	public AudioClip tutorialClip1;
	public AudioClip tutorialClip2;
	public AudioClip tutorialClip3;

	[Space]
    public AudioClip[] levels;

    //TODO: Check tutorial done after finishing script
    private bool tutorialDone = false;
    private GameObject gameMaster;
    private AudioSource source;
    private string sceneName = null;

    private const float CLIP_SKIP = 0.1f;
	private float timer;
	private float TIMERLIMIT = 0.10f;
    private int level;
    private bool changeClip = false;
    private bool lastLevel = false;

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
    void Awake()
    {
        level = -1;
		timer = 0;

        //Gets AudioSource in current GameObject(Coordinator)
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            throw new System.Exception("Couldn't find an AudioSource in Coordinator");
        }

    }


    // Use this for initialization
    void Start()
    {
        //Gets GameMaster object for this script
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        //Gets current active scene name
        sceneName = SceneManager.GetActiveScene().name;

		Tutorial();

        if (gameMaster == null)
        {
            throw new System.Exception("Couldn't find a GameMaster at Coordinator");
        }
        if (sceneName == null)
        {
            throw new System.Exception("Couldn't read scene name at Coordinator");
        }

    }
		
    void Update()
    {

        Debug.Log("Coordinator Current Level -> " + level);

		if (tutorialDone) {
            if(!lastLevel) {
			    if (timer >= TIMERLIMIT) {

				    //Every 0.10sec, compare Coordinator Current Level and GameMaster Current Level, 
				    //if they're different and Coordinator isn't playing anything, get ready to change the current clip

				    if (level != gameMaster.GetComponent<GameMaster> ().GetCurrentLevelIndex () && !source.isPlaying) {
					    changeClip = true;
				    }

				    if (changeClip == false && !source.isPlaying) {
					    UpdateClip ();
				    }

				    //Change current level and update the current clip
				    if (changeClip) {
					    SetCurrentLevel (gameMaster.GetComponent<GameMaster> ().GetCurrentLevelIndex ());
					    UpdateClip ();
					    StartCoroutine (PlayClip ());
					    changeClip = false;
				    }
					
				    timer = 0f;
			    }
            }
		} else {
			if (timer >= TIMERLIMIT && !source.isPlaying) {
				source.clip = introTutorialSound;
				timer = 0f;
			}
		}
		timer += Time.deltaTime;

    }

    public void LastLevel() {
        lastLevel = false;
    }


	IEnumerator PlayClip(){
		yield return new WaitForSeconds (0.5f);
        if (!source.isPlaying)
        {
            PlayCurrentClip();
        }
	}

    public bool isPlaying() {
        return source.isPlaying;
    }

	public void TutorialDone(){
        Debug.Log("Tutorial Done Basıldı");
        tutorialDone = true;
		gameMaster.GetComponent<GameMaster>().TutorialDone ();
  
	}

	private void Tutorial(){
		source.clip = introTutorialSound;
		source.Play ();

	}

    void OnMouseDown()
    {
        //If coordiantor isn't speaking at the moment and coordinator is clicked, PlayCurrentClip
        if (!source.isPlaying)
        {
            PlayCurrentClip();
        }
    }   

	public void PlayTutorialClip1(){
		if (!source.isPlaying) {
			source.clip = tutorialClip1;
			source.Play ();
		}
	}
		
	public void PlayTutorialClip2(){
		if (!source.isPlaying) {
			source.clip = tutorialClip2;
			source.Play ();
		}
	}

	public void PlayTutorialClip3(){
		if (!source.isPlaying) {
			source.clip = tutorialClip3;
			source.Play ();
		}
	}

    // Plays the Succesfull Answer clip
    public void PlaySuccess()
    {
        if (!source.isPlaying)
        {
            source.clip = trueAnswer;
            source.Play();
        }
    }

    // Plays the Wrong Answer clip
    public void PlayFail()
    {
        if (!source.isPlaying)
        {
            source.clip = falseAnswer;
            source.Play();

        }
    }

	//Returns the remaining time in seconds on current audioclip.
	private float timeLeftOnCurrentClip()
	{
		float timeLeft = 0;
		timeLeft = source.clip.length - source.time;
		return timeLeft;
	}

	//Plays the intro clip.
	private void PlayIntro()
	{
		source.clip = introTutorialSound;
		source.Play();
	}

    //Plays the clip for current level
    private void PlayCurrentClip()
    {
        source.Play();
    }

    //Plays the clip for level X 
    public void PlayClipForLevelX(int i)
    {
        source.clip = levels[i - 1];
        source.Play();
    }

    //Sets current level
    public void SetCurrentLevel(int i)
    {
        level = i;
    }

    private void UpdateClip()
    {
		if (level < levels.Length) {
			source.clip = levels [level];
		}
    }
}
