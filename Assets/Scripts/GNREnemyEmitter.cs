using UnityEngine;
using System.Collections;

public class GNREnemyEmitter : MonoBehaviour {
	public GNREnemySeeker enemyPrefab;
	public GNREnemyDot avatarPrefab;
	public Transform avatarOrigin;
	public GameObject target;
	public float spawnRadius = 200f;
	public float spawnHeight = 50;
	public float minDelay = 1f;
	public float maxDelay = 10f;

	public void Start() {
		StartCoroutine(SpawnEnemies());
	}
	
	public IEnumerator SpawnEnemies() {
		while (true) {
			Vector2 pos2d = Random.insideUnitCircle * spawnRadius;
			Vector3 pos3d = new Vector3(pos2d.x, Random.value * spawnHeight, pos2d.y);
			GNREnemySeeker enemy = Instantiate(enemyPrefab, pos3d, Quaternion.identity) as GNREnemySeeker;
			enemy.target = target;
			GNREnemyDot dot = Instantiate(avatarPrefab) as GNREnemyDot;
			dot.transform.parent = avatarOrigin.transform;
			dot.transform.localPosition = Vector3.zero;
			dot.transform.localRotation = Quaternion.identity;
			dot.target = enemy.transform;
			dot.origin = target.transform;
			dot.TrackPosition();
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
		}
	}
	
}
