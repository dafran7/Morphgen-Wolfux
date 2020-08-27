using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {
	public GameObject[] lives;
	public GameObject Heart;
	public GameObject HeartBlack;
	public GameObject player;
	public int maxhp, diff;
	public uint count;
	public static float x;
	public bool re = false;
	public bool hit = false;
	// Use this for initialization
	void Start () {
		x = Heart.transform.position.x;
		maxhp = player.GetComponent<Player>().hp;
		for (int i = 0; i < maxhp; i++) {
			var hearts = (GameObject)Instantiate (Heart, new Vector2 (x, Heart.transform.position.y), Quaternion.identity);
			hearts.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false);
			x += 30;
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		lives = GameObject.FindGameObjectsWithTag ("heart");
		count = 0;
		diff = 0;
	}

	bool kunci = false;
	// Update is called once per frame
	void Update () {
		if (re && !kunci) {
			kunci = true;
			lives [maxhp - count].GetComponent<UnityEngine.UI.Image> ().sprite = Heart.GetComponent<UnityEngine.UI.Image> ().sprite;
			count--;
			diff--;
			player.GetComponent<Player> ().hp += 1;
			re = false;
			kunci = false;
		}
		else if (hit && !kunci) {
			kunci = true;
			diff++;
			lives [maxhp - diff].GetComponent<UnityEngine.UI.Image> ().sprite = HeartBlack.GetComponent<UnityEngine.UI.Image> ().sprite;
			count++;
			hit = false;
			kunci = false;
		}
	}
}
