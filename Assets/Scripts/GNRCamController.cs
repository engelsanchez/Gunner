using UnityEngine;
using System.Collections;

public class GNRCamController : MonoBehaviour {
	
	public float angularSpeed = 1f;
	public float shootFreq = 10f;
	
	float pitch = 0f;
	float yaw = 0f;
	Transform myTransform;
	float shootDelay;
	
	void Awake() {
		myTransform = transform;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		// Unity rounds to perfect 0.0 if near, so comparison OK
		if (h != 0.0f || v != 0.0f) {
			yaw = (yaw + h * angularSpeed * Time.deltaTime) % 360f;
			pitch = Mathf.Clamp(pitch + v * angularSpeed * Time.deltaTime, -25, 90);
			myTransform.parent.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);
			myTransform.localRotation = Quaternion.AngleAxis(pitch, Vector3.left);
		}
		
		if (shootDelay > 0.0000000001 ) {
			shootDelay -= Time.deltaTime;
		} else if(Input.GetButton("Fire1")) {
			BroadcastMessage("Shoot");
			shootDelay = 1f / shootFreq;
		}

	
	}
}
