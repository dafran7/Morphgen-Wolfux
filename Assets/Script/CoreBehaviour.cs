using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreBehaviour : MonoBehaviour {
	public int hp=80;
	public int phase=1;
	public int score;

	public GameObject boss;
	public BossBehaviour bb;
	public SpriteRenderer sr;

	Manager manager;
	public GameObject success;
	public GameObject scoreText;
	Text scrTxt;
	// Use this for initialization
	void Start () {
		scoreText = GameObject.Find("ScoreText");
		scrTxt = scoreText.GetComponent<Text> ();
		score = hp * 10;
		bb = boss.gameObject.GetComponent<BossBehaviour> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator HitNotice(){
		sr.color = Color.red;
		yield return new WaitForSeconds (.2f);
		sr.color = Color.white;
	}

	public void ReduceHP(int dmg){
		hp-=dmg;
		StartCoroutine (HitNotice());
		CheckHP ();
	}

	void CheckHP(){
		if (60 >= hp&&hp > 45)
			phase = 2;
		else if (45 >= hp&&hp > 25)
			phase = 3;
		else if (25 >= hp&&hp > 0)
			phase = 4;
		else if (hp <= 0)
			Dead();

		bb.phase = phase;
		
	}

	void Dead(){
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + score);
		scrTxt.text = "Score : " + PlayerPrefs.GetInt ("Score");
		success.SetActive (true);
		manager.GamePause ();
		Destroy (gameObject);
	}
}
