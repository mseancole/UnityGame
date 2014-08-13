using UnityEngine;
using System.Collections;

public class DestroyUnderZValue : MonoBehaviour {
	public float destroyThreshold = 0.0f;

	// Update is called once per frame
	void Update () {
		if( transform.position.z <= destroyThreshold ) {
			Destroy( gameObject );
		}
	}
}
