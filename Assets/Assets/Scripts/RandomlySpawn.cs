using UnityEngine;
using System.Collections;

public class RandomlySpawn : MonoBehaviour {
	public GameObject entity;
	public float minX = -10.0f;
	public float maxX = 10.0f;
	public float minY = -10.0f;
	public float maxY = 10.0f;

	public float minSpawnTime = 1.0f;
	public float maxSpawnTime = 1.0f;

	// Use this for initialization
	void Start() {
		Invoke( "SpawnNow", Random.Range( minSpawnTime, maxSpawnTime ) );
	}
	
	void SpawnNow() {
		Instantiate( entity, transform.position + new Vector3( Random.Range( minX, maxX ), Random.Range( minY, maxY ) ), Quaternion.identity );
		Invoke( "SpawnNow", Random.Range ( minSpawnTime, maxSpawnTime ) );
	}
}
