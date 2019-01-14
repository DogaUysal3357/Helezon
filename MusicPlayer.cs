using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {


    public Sprite voiceON;
    public Sprite voiceOFF;

    public AudioClip mainMenuSong;

    private AudioSource source;
    private Image image;
    private bool mute = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        if(image == null) {
            Debug.Log("Couldn't find image component in MusicPlayer.");
        }

        source.loop = true;
        source.clip = mainMenuSong;
        source.Play();
    }
	

    public void toggleMute() {
        mute = !mute;

        if(mute) {
            source.volume = .0f;
            image.sprite = voiceOFF;
        } else {
            source.volume = 1f;
            image.sprite = voiceON;
        }
    }





}
