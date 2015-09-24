using UnityEngine;
using System.Collections;

public class VictimController : MonoBehaviour {
	public VictimPath MyPath; // variable set by VictimsMainController
	[Range(1.0f, 1000.0f)]
	public float VictimSpeed = 50.0f;
	int NodeIdx;

	public int VictimHP = 10;
	public int MaxHP = 10;

	public LineRenderer _HpBackground;
	public LineRenderer _HpBar;
	public int PrizeMoney = 10;
	public GameObject VictimSpriteParent;
	// Use this for initialization
	void Start () {
		NodeIdx = 0;
		transform.localPosition = MyPath.PathNodes [NodeIdx].transform.localPosition;
		NodeIdx ++;

		_HpBackground.sortingOrder = 4;
		_HpBar.sortingOrder = 5;
	}
	
	// Update is called once per frame
	void Update () {
		// move
		Vector3 MoveTo = Vector3.MoveTowards (transform.localPosition, MyPath.PathNodes [NodeIdx].transform.localPosition, Time.deltaTime * VictimSpeed);

		if (MoveTo.x >= transform.localPosition.x) {
			VictimSpriteParent.transform.localRotation = Quaternion.AngleAxis (0.0f, Vector3.up);
		} else {
			VictimSpriteParent.transform.localRotation = Quaternion.AngleAxis (180.0f, Vector3.up);
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
			VictimHP = 0;
			Cash.instance.IncCash(PrizeMoney);
			VictimsMainController.instance.AllVictims.Remove(this);
			Destroy(gameObject);
		}

		float HpFactor = (float)VictimHP/(float)MaxHP;

		Color col = new Color (1.0f - HpFactor, HpFactor, HpFactor, 1.0f);

		_HpBar.SetColors (col, col);

		_HpBar.SetPosition(1, new Vector3((HpFactor * 10.0f) - 5.0f, 0.0f, 0.0f));
	}
}
