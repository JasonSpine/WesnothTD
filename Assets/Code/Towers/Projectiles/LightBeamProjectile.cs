using UnityEngine;
using System.Collections;

public class LightBeamProjectile : ProjectileController {

	public override void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform) {
		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (VictimToAttack.gameObject.transform);
		
		Projectile.transform.localPosition = Vector3.zero;
	}
}
