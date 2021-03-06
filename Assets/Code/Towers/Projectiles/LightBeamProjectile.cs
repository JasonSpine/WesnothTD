﻿using UnityEngine;
using System.Collections;

public class LightBeamProjectile : ProjectileController {

	public override void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform, float Damage, float PoisonStrength, float SlowTo) {
		if (VictimToAttack == null)
			return;

		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (VictimToAttack.gameObject.transform);
		
		Projectile.transform.localPosition = Vector3.zero;

		ProjectileController _ProjectileController = Projectile.GetComponent<ProjectileController> ();

		_ProjectileController.VictimToAttack = VictimToAttack;

		_ProjectileController.PoisonStrength = PoisonStrength;
		_ProjectileController.SlowStrength = SlowTo;

		_ProjectileController.Damage = Damage;
	}

	protected override void HitVictimOnCollision(Collider2D other) {
		// no hit here
	}

	protected override void HitVictimAfterAnimation() {
		SlowVictim();
		PoisonVictim();

		AdjustDamage ();

		VictimToAttack.DecHP (Damage);
	}

	protected override void Update ()
	{
		if (VictimToAttack == null) {
			Destroy (gameObject);
			return;
		}

		// animation
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;
			
				if (AnimationIdx >= AnimationSprites.Sprites.Length) {
					AnimationIdx = AnimationSprites.Sprites.Length - 1;
					AnimationPlay = false;
					AnimTimer = AnimInterval;
					HitVictimAfterAnimation ();
				}
			
				ProjectileImage.sprite = AnimationSprites.Sprites [AnimationIdx];
			
				AnimTimer = AnimInterval;
			}
		} else {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				ProjectileImage.sprite = AnimationSprites.Sprites [AnimationIdx];
				AnimationIdx--;
			
				if (AnimationIdx < 0) {
					AnimationIdx = 0;
					ProjectileImage.sprite = AnimationSprites.Sprites [AnimationIdx];
					Destroy (gameObject);
				}
			
				AnimTimer = AnimInterval;
			}
		}
	}
}
