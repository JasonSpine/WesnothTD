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

			VictimController VictimToAttack = VictimsMainController.instance.GetVictimInRange ((Vector2)transform.position, _TowerController.TowerRange, _TowerController.TowerAIBehavior);

			if (VictimToAttack != null) {
				_TowerController.Attack(VictimToAttack);
			}
		}
	}

}
