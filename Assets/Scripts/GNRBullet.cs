using UnityEngine;

using System.Collections;

public class GNRBullet : MonoBehaviour {
	
	public float maxLife = 5f;
	public bool shoot = false;
	public Vector3 shootDirection;
	public float shootForce;
	
	public struct Params {
		public Vector3 dir;
		public float force;
		public Params(Vector3 dir, float speed) {
			this.dir = dir;
			this.force = speed;
		}
	}
	
	public IEnumerator ShootRtn(Vector3 dir, float speed) {
		float start = Time.time;
		Transform myTransform = transform;
		yield return null;
		while( Time.time - start < maxLife) {
			myTransform.Translate(dir * Time.deltaTime * speed, Space.World);
			yield return null;
		}
		Destroy(gameObject, 0f);
	}

	public void FixedUpdate() {
		if (shoot) {
			GetComponent<Rigidbody>().AddForce(shootDirection * shootForce);
			shoot = false;
		}
	}

	public void Shoot(Params p) {
		shoot = true;
		shootDirection = p.dir;
		shootForce = p.force;
	}
}
