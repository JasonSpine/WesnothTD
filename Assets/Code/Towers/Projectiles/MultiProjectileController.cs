using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiProjectileController : ProjectileController {
	public GameObject SubProjectilePrefab;
	public int SubProjectilesCount = 3;

	public override void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform, float Damage, float PoisonStrength, float SlowTo) {
		if (VictimToAttack == null)
			return;

		List<VictimController> VictimsToAttack = new List<VictimController>();
		
		for (int i = 0; i < SubProjectilesCount; i++) {
			VictimController SubVictim = VictimsMainController.instance.GetClosestVictim((Vector2)VictimToAttack.transform.position, VictimsToAttack);
			if (SubVictim != null) {
				VictimsToAttack.Add(SubVictim);
			} else {
				break;
			}
		}

		foreach (VictimController vToAttack in VictimsToAttack) {
			GameObject Projectile = (GameObject)Instantiate (SubProjectilePrefab);
			Projectile.transform.SetParent (MapParentHandle.instance.gameObject.transform);
		
			Projectile.transform.position = TowerTransform.position;

			ProjectileController _ProjectileController = Projectile.GetComponent<ProjectileController> ();

			_ProjectileController.VictimToAttack = vToAttack;
		
			_ProjectileController.Damage = Damage;
			_ProjectileController.PoisonStrength = PoisonStrength;
			_ProjectileController.SlowStrength = SlowTo;

			Projectile.transform.localRotation = Quaternion.AngleAxis (
				Mathf.Rad2Deg * Mathf.Atan2 (vToAttack.transform.position.y - Projectile.transform.position.y,
			                             vToAttack.transform.position.x - Projectile.transform.position.x),
			Vector3.forward);

			foreach (ParticleSystem PS in Projectile.GetComponentsInChildren<ParticleSystem>(true)) { // to prevent trail when set start position
				PS.gameObject.SetActive (true);
			}
		}
	}
}
