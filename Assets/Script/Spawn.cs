using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	public GameObject apalah;
	// Use this for initialization
	void Start () {
		StartCoroutine (Spawning());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator Spawning(){
		yield return new WaitForSeconds(2f);
		Instantiate (apalah, transform.position, Quaternion.identity);
		StartCoroutine (Spawning());
	}
}
