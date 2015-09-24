using UnityEngine;
using System.Collections;

public class VictimController : MonoBehaviour {
	public VictimPath MyPath; // variable set by VictimsMainController
	[Range(1.0f, 1000.0f)]
	public float VictimSpeed = 50.0f;
	int NodeIdx;

	public int VictimHP = 10;
	public int PrizeMoney = 10;
	// Use this for initialization
	void Start () {
		NodeIdx = 0;
		transform.localPosition = MyPath.PathNodes [NodeIdx].transform.localPosition;
		NodeIdx ++;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 MoveTo = Vector3.MoveTowards (transform.localPosition, MyPath.PathNodes [NodeIdx].transform.localPosition, Time.deltaTime * VictimSpeed);

		if (MoveTo.x >= transform.localPosition.x) {
			transform.localRotation = Quaternion.AngleAxis (0.0f, Vector3.up);
		} else {
			transform.localRotation = Quaternion.AngleAxis (180.0f, Vector3.up);
		}

		transform.localPosition = MoveTo;

		if (Vector3.Distance (transform.localPosition, MyPath.PathNodes [NodeIdx].transform.localPosition) <= 0.01f) {
			if (NodeIdx < MyPath.PathNodes.Length - 1) {
				NodeIdx ++;
			} else {
				NodeIdx = 0;
				transform.localPosition = MyPath.PathNodes [NodeIdx].transform.localPosition;
				Cash.instance.DecLives(1);
			}
		}
	}

	public void DecHP(int Damage) {
		VictimHP -= Damage;

		if (VictimHP <= 0) {
			Cash.instance.IncCash(PrizeMoney);
			VictimsMainController.instance.AllVictims.Remove(this);
			Destroy(gameObject);
		}
	}
}
