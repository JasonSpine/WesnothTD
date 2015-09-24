using UnityEngine;
using System.Collections;

public class LightBeamProjectile : ProjectileController {

	public override void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform, int Damage) {
		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (VictimToAttack.gameObject.transform);
		
		Projectile.transform.localPosition = Vector3.zero;

		Projectile.GetComponent<ProjectileController> ().VictimToAttack = VictimToAttack;

		Projectile.GetComponent<ProjectileController> ().Damage = Damage;
	}

	protected override void HitVictimOnCollision() {
		// no hit here
	}

	protected override void HitVictimAfterAnimation() {
		VictimToAttack.DecHP (Damage);
	}
}
