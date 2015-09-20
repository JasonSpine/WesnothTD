using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class VictimsMainController : MonoBehaviour {
	public static VictimsMainController instance;

	public VictimPath VictimPath1, VictimPath2;

	Button SendWaveButtonReference = null;

	public List <VictimController> AllVictims;
	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendWaveButtonAction(Button Caller) {
		SendWaveButtonReference = Caller;

		SendWaveButtonReference.interactable = false;
	}

	public VictimController GetColosestVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultDistance = radius + 10.0f;

		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					resultDistance = DistFromTowerToVictim;
				}
			} else { // if tower in range is found, look for the closest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					if (resultDistance > DistFromTowerToVictim) {
						result = v;
						resultDistance = DistFromTowerToVictim;
					}
				}
			}
		}

		return result;
	}
}
