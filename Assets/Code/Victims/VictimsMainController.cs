﻿using UnityEngine;
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

	public GameObject[] VictimPrefabs;


	[Header("Waves")]
	[Range(0.01f, 4.0f)]
	public float PairReleaseInterval;
	float PairReleaseIntervalTimer = 0.0f;
	public int VictimPairsAmountInEachWave;
	protected int VictimPairsToReleaseInCurrentWave;

	public int WavesCount = 50;
	public VictimEnumID[] WavesPattern;

	[SerializeField]
	int CurrentWave = -1;

	public float VictimsHp = 10.0f;
	bool WaveRelease = false;

	[Range(1.0f, 5.0f)]
	public float VictimsHpIncreaseMultiplier = 1.2f;
	public int VictimsPrize = 5;
	public int VictimsPrizeIncrease = 1;

	public WaveInfo CurrentWaveInfo, NextWaveInfo;


	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		SetInfoFieldsForWave (NextWaveInfo, 0);
	}

	public void SetInfoFieldsForWave(WaveInfo waveInfo, int WaveNumber) {
		if ((WaveNumber >= 0) && (WaveNumber <= WavesCount)) {
			VictimEnumID vID = WavesPattern [WaveNumber % WavesPattern.Length];

			foreach (GameObject victim in VictimPrefabs) {
				VictimController vCon = victim.GetComponent<VictimController> ();
				if (vCon.VictimID == vID) {
					waveInfo.SetFields (vCon.VictimSprite.sprite, GetVictimsHp(WaveNumber), GetVictimsPrize(WaveNumber), WaveNumber);
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (WaveRelease) {
			VictimsReleaser ();
		} else { // check win condition
			if (CurrentWave + 1 >= WavesCount) {
				if (AllVictims.Count <= 0) {
					PauseController.WinGame = true;
				}
			}
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

	float GetVictimsHp(int WaveNumber) {
		float HpIncreased = VictimsHp;
		for (int i = 0; i < WaveNumber; i++) {
			HpIncreased *= VictimsHpIncreaseMultiplier;
		}
		return HpIncreased;
	}

	int GetVictimsPrize(int WaveNumber) {
		return VictimsPrize + (VictimsPrizeIncrease * WaveNumber);
	}

	void AddVictim(VictimEnumID VictimID, VictimPath _VictimPath) {
		foreach (GameObject victim in VictimPrefabs) {
			if (victim.GetComponent<VictimController>().VictimID == VictimID) {
				GameObject VictimObj = (GameObject)Instantiate(victim);
				VictimObj.transform.SetParent(transform);

				VictimController _VictimController = VictimObj.GetComponent<VictimController>();

				_VictimController.MyPath = _VictimPath;

				AllVictims.Add(_VictimController);

				_VictimController.Initialize (GetVictimsHp(CurrentWave), GetVictimsPrize(CurrentWave), VictimPairsToReleaseInCurrentWave);

				break;
			}
		}
	}

	public void SendWaveButtonAction(Button Caller) {
		CurrentWave ++;
		SendWaveButtonReference = Caller;

		SendWaveButtonReference.interactable = false;
		WaveRelease = true;

		VictimPairsToReleaseInCurrentWave = VictimPairsAmountInEachWave;

		SetInfoFieldsForWave (CurrentWaveInfo, CurrentWave);
		SetInfoFieldsForWave (NextWaveInfo, CurrentWave + 1);

		if ((CurrentWave + 1) >= WavesCount) {
			SendWaveButtonReference.gameObject.SetActive(false);
		}
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
			} else { // if any tower in range is found, look for the closest
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

	public VictimController GetFurthestVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultDistance = -10.0f;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					resultDistance = DistFromTowerToVictim;
				}
			} else { // if any tower in range is found, look for the furthest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					if (resultDistance < DistFromTowerToVictim) {
						result = v;
						resultDistance = DistFromTowerToVictim;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetFastestVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultSpeed = 0.0f;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimSpeed = v.GetVictimSpeed();
					resultSpeed = VictimSpeed;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimSpeed = v.GetVictimSpeed();
					if (VictimSpeed > resultSpeed) {
						result = v;
						resultSpeed = VictimSpeed;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetSlowestVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultSpeed = float.MaxValue;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimSpeed = v.GetVictimSpeed();
					resultSpeed = VictimSpeed;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimSpeed = v.GetVictimSpeed();
					if (VictimSpeed < resultSpeed) {
						result = v;
						resultSpeed = VictimSpeed;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetMostPoisonedVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultPoison = 0.0f;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimPoison = v.GetVictimPoison();
					resultPoison = VictimPoison;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimPoison = v.GetVictimPoison();
					if (VictimPoison > resultPoison) {
						result = v;
						resultPoison = VictimPoison;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetLessPoisonedVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultPoison = float.MaxValue;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimPoison = v.GetVictimPoison();
					resultPoison = VictimPoison;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimPoison = v.GetVictimPoison();
					if (VictimPoison < resultPoison) {
						result = v;
						resultPoison = VictimPoison;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetMostHpVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultHp = -1.0f;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimHp = v.VictimHP;
					resultHp = VictimHp;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimHp = v.VictimHP;
					if (VictimHp > resultHp) {
						result = v;
						resultHp = VictimHp;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetLessHpVictimInRange(Vector2 from, float radius) {
		VictimController result = null;
		float resultHp = float.MaxValue;
		
		foreach (VictimController v in AllVictims) {
			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					result = v;
					float VictimHp = v.VictimHP;
					resultHp = VictimHp;
				}
			} else { // if any tower in range is found, look for the fastest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < radius) {
					float VictimHp = v.VictimHP;
					if (VictimHp < resultHp) {
						result = v;
						resultHp = VictimHp;
					}
				}
			}
		}
		
		return result;
	}

	public VictimController GetVictimInRange(Vector2 from, float radius, TowerController.AIBehavior TowerAIBehavior) {
		VictimController result = null;

		switch (TowerAIBehavior) {
		case TowerController.AIBehavior.FASTEST:
			result = GetFastestVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.SLOWEST:
			result = GetSlowestVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.LESS_HP:
			result = GetLessHpVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.MORE_HP:
			result = GetMostHpVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.LESS_POISONED:
			result = GetLessPoisonedVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.MORE_POISONED:
			result = GetMostPoisonedVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.FURTHEST:
			result = GetFurthestVictimInRange(from, radius);
			break;
		case TowerController.AIBehavior.CLOSEST:
		default:
			result = GetColosestVictimInRange(from, radius);
			break;

		}

		return result;
	}

	public VictimController GetClosestVictim(Vector2 from, List<VictimController> ExceptVictims = null) { // for projectiles that lose their target
		VictimController result = null;
		float resultDistance = float.MaxValue;
		
		foreach (VictimController v in AllVictims) {
			if (ExceptVictims != null) {
				if (ExceptVictims.Contains(v)) {
					continue;
				}
			}

			if (result == null) { // repeat until find tower in range
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < resultDistance) {
					result = v;
					resultDistance = DistFromTowerToVictim;
				}
			} else { // if any tower in range is found, look for the closest
				float DistFromTowerToVictim = Vector2.Distance(from, (Vector2)v.transform.position);
				if (DistFromTowerToVictim < resultDistance) {
					result = v;
					resultDistance = DistFromTowerToVictim;
				}
			}
		}
		
		return result;
	}
}
