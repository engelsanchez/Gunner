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
	
	public void Awake () {
		myTransform = transform;
		origColor = renderer.material.color;
	}
	
	public void Update () {
		Vector3 dir = target.transform.position - myTransform.position;
		float distance = dir.magnitude;
		if (distance > near) {
			float delta = Mathf.Clamp(speed * Time.deltaTime, 0, distance - near);
			myTransform.Translate(dir.normalized * delta);
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
		return x < .5f ? 2 * x : 2 - 2 * x;
	}
}
