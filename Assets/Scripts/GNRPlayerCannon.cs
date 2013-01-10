using UnityEngine;
using System.Collections;

public class GNRPlayerCannon : MonoBehaviour {
	public GNRBullet bulletPrefab;
	public float bulletSpeed = 50f;
	
	public void Shoot() {
		GNRBullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GNRBullet;
		bullet.Shoot(new GNRBullet.Params(transform.forward, bulletSpeed));
	}
}
