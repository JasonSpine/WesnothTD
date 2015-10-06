using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void VisitWesnothOrgButtonAction() {
		Application.OpenURL ("http://wesnoth.org/");
	}

	public void VisitGitHubPageButtonAction() {
		Application.OpenURL ("https://github.com/JasonSpine/WesnothTD");
	}

	public void QuitButtonAction() {
		Application.Quit ();
	}

	public void PlayLevelAction(int SceneID) {
		Application.LoadLevel (SceneID);
	}
}
