using UnityEngine;
using System.Collections;

public class FieldsMainController : MonoBehaviour {
	public static float MapWidth = -1.0f;
	public static float MapHeight = 1.0f;
	// Use this for initialization
	void Start () {
		foreach (Transform field in GetComponentsInChildren<Transform> ()) {
			if (field.localPosition.x > MapWidth) {
				MapWidth = field.localPosition.x;
			}

			if (field.localPosition.y < MapHeight) {
				MapHeight = field.localPosition.y;
			}
		}

		MapHeight = Mathf.Abs (MapHeight);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
