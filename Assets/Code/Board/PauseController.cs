using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PauseController : MonoBehaviour {
	public static bool WinGame, LoseGame;
	public GameObject PauseOverlay;
	public Text PauseText;
	// Use this for initialization
	void Awake() {
		PauseController.WinGame = false;
		PauseController.LoseGame = false;
	}

	void Start () {
		Time.timeScale = 1.0f;
		PauseOverlay.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (WinGame) {
			if (!PauseOverlay.activeSelf) {
				PauseText.text = "You Win!";
				PauseOverlay.SetActive(true);
				Time.timeScale = 0.0f;
			}
		} else if (LoseGame) {
			if (!PauseOverlay.activeSelf) {
				PauseText.text = "You Lose...";
				PauseOverlay.SetActive(true);
				Time.timeScale = 0.0f;
			}
		} else {
			if ((!PauseOverlay.activeSelf) &&
				(Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Pause))) {
				PauseOverlay.SetActive (true);
				Time.timeScale = 0.0f;
			} else if (PauseOverlay.activeSelf) {
				if (Input.anyKeyDown && !Input.GetMouseButtonDown (0)) {
					PauseOverlay.SetActive (false);
					Time.timeScale = 1.0f;
				}
			}
		}
	}

	public void RestartLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void BackToMenu() {
		Application.LoadLevel (0);
	}

	public void QuitTheGame() {
		Application.Quit ();
	}
}
