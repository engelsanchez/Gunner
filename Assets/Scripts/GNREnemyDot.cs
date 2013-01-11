using UnityEngine;
using System.Collections;

public class GNREnemyDot : MonoBehaviour {
	public Transform target;
	public Transform origin;
	public float range = 100f;
	
	public void Update () {
		// track target
		if (!target) {
			Destroy(gameObject);
			return;
		}
		Vector3 pos = transform.localPosition;
		Vector3 ofs3D = target.position - origin.position;
		Vector2 ofs = new Vector2(ofs3D.x, ofs3D.z) / range;
		Vector3 forward3D = transform.forward;
		Vector2 up = new Vector2(forward3D.x, forward3D.z);
		Vector2 right = new Vector2(up.y, -up.x);
		pos.x = Proj2(ofs, right);
		pos.y = Proj2(ofs, up);
		transform.localPosition = pos;
	}
	
	public static float Proj2(Vector2 v, Vector2 refv) {
		return Vector2.Dot(v, refv) * refv.magnitude;
	}
}
