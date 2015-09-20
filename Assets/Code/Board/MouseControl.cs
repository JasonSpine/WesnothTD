using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
	public float BoundarySize = 20.0f;

	Vector3 LastMousePos;
	bool Clicked = false;
	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3(-BoundarySize, BoundarySize, - 10.0f);
		transform.position = new Vector3(32.0f -CameraController.instance._CanvasScaler.referenceResolution.x / 2.0f,
		                                 -32.0f +CameraController.instance._CanvasScaler.referenceResolution.y / 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x < (float)Screen.width - (270.0f * (float)Screen.width/CameraController.instance._CanvasScaler.referenceResolution.x)) {
			if (Input.GetMouseButton (0)) {
				if (!Clicked) {
					LastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					Clicked = true;
				} else {
					Vector2 MouseShift = -(Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - LastMousePos);

					Vector2 NewPos = MouseShift + (Vector2)Camera.main.transform.position;

					if (NewPos.x < -BoundarySize) {
						NewPos.x = -BoundarySize;
					} else {
						//if FieldsMainController.MapWidth < 
					}

					if (NewPos.y > BoundarySize) {
						NewPos.y = BoundarySize;
					}

					Camera.main.transform.position = new Vector3(NewPos.x, NewPos.y, -10.0f);



					LastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
			} else {
				Clicked = false;
			}
		} else {
			Clicked = false;
		}
	}
}
