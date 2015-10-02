using UnityEngine;
using System.Collections;

public class VictimController : MonoBehaviour {
	public VictimPath MyPath; // variable set by VictimsMainController
	[Range(1.0f, 1000.0f)]
	public float VictimSpeed = 50.0f;
	int NodeIdx;

	public float VictimHP = 10.0f;
	public float MaxHP = 10.0f;

	public LineRenderer _HpBackground;
	public LineRenderer _HpBar;
	public int PrizeMoney = 10;
	public GameObject VictimSpriteParent;
	public SpriteRenderer VictimSprite;

	bool Poisoned;
	float PoisonTimeLeft = 0.0f;
	float PoisonHpLoseRate = 0.0f;

	bool Slowed;
	[SerializeField]
	float SlowSpeed;
	float RegainSpeedRate = 4.0f;

	// Use this for initialization in group controller
	public void Initialize (float Hp, int Prize, int SortingOrder) {
		NodeIdx = 0;
		transform.localPosition = MyPath.PathNodes [NodeIdx].transform.localPosition;
		NodeIdx ++;

		VictimSprite.sortingOrder = SortingOrder * 4;
		_HpBackground.sortingOrder = (SortingOrder * 4) + 1;
		_HpBar.sortingOrder = (SortingOrder * 4) + 2;

		SlowSpeed = VictimSpeed;

		MaxHP = VictimHP = Hp;
		PrizeMoney = Prize;
	}
	
	// Update is called once per frame
	void Update () {
		// HandlePoison
		if (Poisoned) {
			if (PoisonTimeLeft > 0.0f) {
				PoisonTimeLeft -= Time.deltaTime;
			} else {
				Poisoned = false;
				PoisonTimeLeft = 0.0f;
				PoisonHpLoseRate = 0.0f;
				VictimSprite.color = Color.white;
			}

			DecHP(Time.deltaTime * PoisonHpLoseRate);
		}

		// Handle Slow
		if (Slowed) {
			if (SlowSpeed < VictimSpeed) {
				SlowSpeed += Time.deltaTime * RegainSpeedRate;
			} else {
				Slowed = false;
				SlowSpeed = VictimSpeed;
			}
		}

		// move
		Vector3 MoveTo = Vector3.MoveTowards (transform.localPosition, MyPath.PathNodes [NodeIdx].transform.localPosition, Time.deltaTime * (Slowed?SlowSpeed:VictimSpeed));

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

	public void DecHP(float Damage) {
		VictimHP -= Damage;

		if (VictimHP <= 0.0f) {
			VictimHP = 0.0f;
			Cash.instance.IncCash(PrizeMoney);
			VictimsMainController.instance.AllVictims.Remove(this);
			Destroy(gameObject);
		}

		float HpFactor = VictimHP/MaxHP;

		Color col = new Color (1.0f - HpFactor, HpFactor, HpFactor, 1.0f);

		_HpBar.SetColors (col, col);

		_HpBar.SetPosition(1, new Vector3((HpFactor * 10.0f) - 5.0f, 0.0f, 0.0f));
	}

	public void Poison(float PoisonStrength) {
		VictimSprite.color = new Color (0.5f, 1.0f, 0.5f, 1.0f);
		Poisoned = true;

		PoisonTimeLeft += 3.0f;
		PoisonHpLoseRate += PoisonStrength;
	}

	public void SlowDown(float SlowTo) {
		Slowed = true;

		SlowSpeed = Mathf.Min (SlowSpeed, SlowTo);
	}

	public float GetVictimSpeed() {
		if (Slowed) {
			return SlowSpeed;
		} else {
			return VictimSpeed;
		}
	}

	public float GetVictimPoison() {
		if (Poisoned) {
			return PoisonHpLoseRate;
		} else {
			return 0.0f;
		}
	}
}
