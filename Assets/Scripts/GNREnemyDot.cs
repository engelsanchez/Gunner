using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy dot on the radar screen that tracks the position of
/// the enemy relative to the player.
/// </summary>
public class GNREnemyDot : MonoBehaviour {
	public Transform target;
	public Transform origin;
	public float range = 100f;
	public float visibilityRadius = 1f;
	
	Renderer[] renderers;
	
	public void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
	}
	
	public void TrackPosition() {
		Vector3 pos = transform.localPosition;
		Vector3 ofs3D = target.position - origin.position;
		Vector2 ofs = new Vector2(ofs3D.x, ofs3D.z) / range;
		Vector3 forward3D = origin.forward;
		Vector2 up = new Vector2(forward3D.x, forward3D.z);
		Vector2 right = new Vector2(up.y, -up.x);
		pos.x = Proj2(ofs, right);
		pos.y = Proj2(ofs, up);
		transform.localPosition = pos;
		bool visible = pos.x * pos.x + pos.y * pos.y <= visibilityRadius * visibilityRadius;
		foreach (Renderer r in renderers) {
			r.enabled = visible;
		}
	}
	
	public void Update () {
		// track target
		if (!target) {
			Destroy(gameObject);
			return;
		}
		TrackPosition();
	}
	
	public static float Proj2(Vector2 v, Vector2 refv) {
		return Vector2.Dot(v, refv) * refv.magnitude;
	}
}
