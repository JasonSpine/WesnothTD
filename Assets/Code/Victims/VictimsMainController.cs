using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class VictimsMainController : MonoBehaviour {
	public static VictimsMainController instance;

	public VictimPath VictimPath1, VictimPath2;

	Button SendWaveButtonReference = null;

	public List <VictimController> AllVictims;

	[System.Serializable]
	public enum VictimEnumID {
		UNDEAD,
		LIZARD,
		WOLF,
		DRAKE,
		LOYALIST,
		TROLL
	}

	[System.Serializable]
	public class VictimPrefabItem {
		public VictimEnumID VictimID;
		public GameObject VictimPrefab;
	}

	public VictimPrefabItem[] VictimPrefabs;


	[Header("Waves")]
	[Range(0.01f, 4.0f)]
	public float PairReleaseInterval;
	float PairReleaseIntervalTimer = 0.0f;
	public int VictimPairsAmountInEachWave;
	protected int VictimPairsToReleaseInCurrentWave;

	public int WavesCount;
	public VictimEnumID[] WavesPattern;
	int CurrentWave = -1;

	public float VictimsHp = 10.0f;
	bool WaveRelease = false;

	[Range(1.0f, 5.0f)]
	public float VictimsHpIncreaseMultiplier = 1.2f;
	public int VictimsPrize = 5;
	public int VictimsPrizeIncrease = 1;


	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (WaveRelease) {
			VictimsReleaser();
		}
	}

	void VictimsReleaser() {
		if (PairReleaseIntervalTimer > 0.0f) {
			PairReleaseIntervalTimer -= Time.deltaTime;
		} else {
			VictimPairsToReleaseInCurrentWave --;

			AddVictim (WavesPattern[CurrentWave % WavesPattern.Length], VictimPath1);
			AddVictim (WavesPattern[CurrentWave % WavesPattern.Length], VictimPath2);

			if (VictimPairsToReleaseInCurrentWave <= 0) {
				WaveRelease = false;
				SendWaveButtonReference.interactable = true;
				PairReleaseIntervalTimer = 0.0f;
			} else {
				PairReleaseIntervalTimer = PairReleaseInterval;
			}
		}
	}

	void AddVictim(VictimEnumID VictimID, VictimPath _VictimPath) {
		GameObject VictimPrefab;

		foreach (VictimPrefabItem item in VictimPrefabs) {
			if (item.VictimID == VictimID) {
				GameObject VictimObj = (GameObject)Instantiate(item.VictimPrefab);
				VictimObj.transform.SetParent(transform);

				VictimController _VictimController = VictimObj.GetComponent<VictimController>();

				_VictimController.MyPath = _VictimPath;

				AllVictims.Add(_VictimController);

				_VictimController.Initialize (VictimsHp, VictimsPrize, VictimPairsToReleaseInCurrentWave);
			}
		}
	}

	public void SendWaveButtonAction(Button Caller) {
		CurrentWave ++;
		SendWaveButtonReference = Caller;

		SendWaveButtonReference.interactable = false;
		WaveRelease = true;

		VictimPairsToReleaseInCurrentWave = VictimPairsAmountInEachWave;
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
