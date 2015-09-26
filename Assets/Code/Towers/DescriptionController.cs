using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {
	public Text DescriptionValues;

	public void SetDescriptionValues(int Price, float Damage) {
		DescriptionValues.text =
			"$" + Price.ToString () + "\n"
			+ Damage.ToString ();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
