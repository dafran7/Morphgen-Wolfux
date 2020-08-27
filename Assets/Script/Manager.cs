using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public bool pause=false;

	public GameObject pauseButton;
	public GameObject pauseWindow;
	// Use this for initialization
	void Start () {
		
	}
	public void GamePause(){
		pause = true;
		Time.timeScale = 0f;

	}
	public void GameResume(){
		pause = false;
		Time.timeScale = 1f;
	}
	public void Restart(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}
	public void MainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene (0);
	}
	// Update is called once per frame
	void Update () {

	}

	public void ShowPauseWindow(){
		pauseButton.SetActive (false);
		pauseWindow.SetActive (true);
		GamePause ();
	}
	public void ClosePauseWindow(){
		pauseButton.SetActive (true);
		pauseWindow.SetActive (false);
		GameResume ();
	}

}
