using UnityEngine;
using System.Collections;

public class ObjectsSpawner : MonoBehaviour {

	public int distance = 15;
	private GameObject player;
	private Vector3 oldPos;
	private Vector3 treePos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		oldPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curPos = player.transform.position;

		if (Mathf.Sqrt (Mathf.Pow (curPos.x - treePos.x, 2)) > distance / 2) {
			foreach (Transform child in transform) {
				if (child.gameObject.layer == LayerMask.NameToLayer ("Background") && Random.Range (0, 2) == 0) {
					Transform newObject = Instantiate (child, new Vector3 (player.transform.position.x + Random.Range (15.0f, 25.0f), 3.0f, -0.1f), Quaternion.identity) as Transform;
					newObject.name = child.name + "#" + newObject.transform.position;
					newObject.SetParent (this.transform);
					break;
				}
			}
			treePos = player.transform.position;
		}
		if (Mathf.Sqrt (Mathf.Pow (curPos.x - oldPos.x, 2)) > distance) {
			foreach (Transform child in transform) {
				if (child.gameObject.layer == LayerMask.NameToLayer("Objects") && Random.Range(0, 7) == 5) {
					Transform newObject = Instantiate (child, new Vector3(player.transform.position.x + Random.Range(15.0f, 50.0f), 2.0f, -1.0f), Quaternion.identity) as Transform;
					newObject.name = child.name + "#" + newObject.transform.position;
					newObject.SetParent(this.transform);
					break;
				}
			}
			oldPos = player.transform.position;
		}
		cleanupObjects ();
	}

	private void cleanupObjects() {
		foreach (Transform child in transform) {
			if (Mathf.Sqrt (Mathf.Pow (player.transform.position.x - oldPos.x, 2)) > distance * 2) {
				Destroy (child.gameObject);
			}
		}
	}
}
