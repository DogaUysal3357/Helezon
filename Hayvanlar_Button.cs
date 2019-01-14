using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hayvanlar_Button : MonoBehaviour {

    //Different Button Sprites
    public Sprite[] sprites;
    //Sprites index numbers.
    public int[] spriteIndexes;
    //Sprites Position Array
    public Vector3[] spritePos;
    //Sprites Scale Array
    public Vector3[] spriteScale;


    private GameMaster gm;
    private int lvIndex;
    private SpriteRenderer sr;
    private BoxCollider2D col;



    void Start () {
        gm = GameObject.Find("_GameMaster").GetComponent<GameMaster>();
        lvIndex = -1;
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        lvIndex = gm.GetCurrentLevelIndex();
        CheckSprite();

	}




    void CheckSprite() {

        for(int i=0; i<spriteIndexes.Length; ++i) {
            if(lvIndex > spriteIndexes[i]) {
                sr.sprite = sprites[i];
                gameObject.transform.position = spritePos[i];
                gameObject.transform.localScale = spriteScale[i];
                if(col != null) {
                    col.size = sprites[i].bounds.size;                  
                }
            }
        }
    }



}
