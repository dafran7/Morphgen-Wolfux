using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
	public GameObject telepos;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.tag == "Player") {
			col.gameObject.GetComponent<Player> ().isJumping = false;
			col.gameObject.GetComponent<Player> ().dbJump = false;
			col.gameObject.transform.position = telepos.transform.position;
			AudioSource aud = col.gameObject.GetComponent<AudioSource> ();
			aud.enabled = false;
			AudioSource auc = canvas.GetComponent<AudioSource> ();
			auc.enabled = true;
		}
	}
}
