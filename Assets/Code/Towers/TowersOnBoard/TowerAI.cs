using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {
	public TowerController _TowerController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_TowerController.CanAttack) {
			VictimController VictimToAttack = VictimsMainController.instance.GetColosestVictimInRange ((Vector2)transform.position, _TowerController.TowerRange);
			if (VictimToAttack != null) {
				_TowerController.CanAttack = false;
				_TowerController.AttackIntervalTimer = _TowerController.AttackInterval;
			}
		} else {
			if (_TowerController.AttackIntervalTimer > 0.0f) {
				_TowerController.AttackIntervalTimer -= Time.deltaTime;
			} else {
				_TowerController.CanAttack = true;
			}
		}
	}

}
