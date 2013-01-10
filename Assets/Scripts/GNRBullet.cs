using UnityEngine;
using System.Collections;

public class GNRBullet : MonoBehaviour {
	
	public float maxLife = 5f;
	
	public struct Params {
		public Vector3 dir;
		public float speed;
		public Params(Vector3 dir, float speed) {
			this.dir = dir;
			this.speed = speed;
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
	
	public void Shoot(Params p) {
		StartCoroutine(ShootRtn(p.dir, p.speed));
	}
}
