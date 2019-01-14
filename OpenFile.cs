using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenTutorial() {
        Application.OpenURL("Helezon_Data\\OYUN KILAVUZU.pdf");
    }
}
