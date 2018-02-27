using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// Forces GameObject to have a BoxCollides2D component
// In other words, Button script requires the GameObject to have a BoxCollider2D component
[RequireComponent(typeof(BoxCollider2D))]
public class Button : MonoBehaviour {
	
	//GameMaster script component
	private GameMaster gm;

	void Start () {
	
		//Gets GameMaster script component in GameMaster GameObject
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();


		if (gm == null) {
			throw new System.Exception ("Couldn't find GameMaster script at Button");
		}
	}


	// ON Mouse Click this function runs
	void OnMouseDown(){

		// if theres anything between this object and mouse cursor, skip this script

		/*if (EventSystem.current.IsPointerOverGameObject ())
			return;
		*/

		// Calls the correct checker function in GameMaster
		if (gameObject.tag == "Button1") {
			gm.Button1Pressed ();
		} else if (gameObject.tag == "Button2") {
			gm.Button2Pressed ();
		} else {
			gm.Button3Pressed ();
		}

	}

}
