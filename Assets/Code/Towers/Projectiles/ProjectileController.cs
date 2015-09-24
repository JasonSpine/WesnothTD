using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
	public virtual void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform) {
		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (MapParentHandle.instance.gameObject.transform);
		
		Projectile.transform.position = TowerTransform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
