using UnityEngine;
using System.Collections;

public class GNREnemySeeker : MonoBehaviour {
	
	public GameObject target;
	public float speed = 1f;
	public float near = 2f;
	public float bulletDamage = 10f;
	
	float hitpoints = 100f;
	Transform myTransform;
	Color origColor;
	float origHeight = 0f;
	float minHeight = 1f;
	
	public float GroundHeight {
		get {
			Vector3 pos = myTransform.position;
			RaycastHit hit = new RaycastHit();
			int onlyTerrain = ~LayerMask.NameToLayer("Terrain");
			float ofs = 1.0f;
			if (Physics.Raycast(pos + Vector3.up * ofs, Vector3.down, out hit, float.MaxValue, onlyTerrain)) {
				return hit.distance - ofs;
			} else if (Physics.Raycast(pos + Vector3.down * ofs, Vector3.up, out hit, float.MaxValue, onlyTerrain)) {
				return -hit.distance + ofs;
			} else 
				return origHeight;
		}
	}
	
	public void Awake () {
		myTransform = transform;
		origColor = renderer.material.color;
	}
	
	public void Start() {
		origHeight = GroundHeight;
	}
	
	public void Update () {
		Vector3 dir = target.transform.position - myTransform.position;
		float distance = dir.magnitude;
		if (distance > near) {
			float delta = Mathf.Clamp(speed * Time.deltaTime, 0, distance - near);
			myTransform.Translate(dir.normalized * delta);
		}
		float h = GroundHeight;
		Debug.Log("Current height is "+h+", orig is "+origHeight+" "+myTransform.position);
		float dh = origHeight - h;
		if (Mathf.Abs(dh) > 1e-3) {
			myTransform.Translate(Vector3.up * dh, Space.World);
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		hitpoints -= bulletDamage;	
		if (hitpoints <= 0f) {
			Destroy(gameObject);
			return;
		}
		
		if (hitAction)
			StopCoroutine("HitAction");
		StartCoroutine("HitAction");
	}
	
	
	bool hitAction = false;
	
	public IEnumerator HitAction() {
		Material mat = renderer.material;
		Color targetColor = Color.yellow;
		float t0 = Time.time;
		float maxTime = 0.1f;
		float elapsed = 0f;
		
		do {
			yield return null;
			elapsed = Time.time - t0;
			mat.color = Color.Lerp(origColor, targetColor, Curve(elapsed / maxTime));
		} while(elapsed < maxTime);
		
	}
	
	public static float Curve(float x) {
		x = Mathf.Clamp01(x);
		// Simple Triangular shape (zero to one, then down to zero
		return x < .5f ? 2 * x : 2 - 2 * x;
	}
	
	public void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "EnemyIcon.png", false);
	}
}
