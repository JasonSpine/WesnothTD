using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
	public static CameraController instance;
	public Camera cam;

	public CanvasScaler _CanvasScaler;
	// Use this for initialization
	void Awake () {
		instance = this;
		cam.orthographicSize = 0.5f * _CanvasScaler.referenceResolution.x * ((float)Screen.height/(float)Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
