using UnityEngine;
using System.Collections;

public class TerrarinGenerator : MonoBehaviour {

	public Vector2 size = new Vector2(8.0f, 2.0f);
	public float distance = 3 * 8.0f;

	public int groundAdded = -3;
	public int groundBack = -3;

	private Transform ground;
	private Transform player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		ground = transform.FindChild ("Ground");
		ground.localScale = new Vector3 (size.x, size.y, 1.0f);

		for (; groundAdded < 3;) {
			Transform newGround = Instantiate (ground, new Vector3(groundAdded * size.x * 3.2f, 0, 0), Quaternion.identity) as Transform;
			newGround.name = "Ground#" + groundAdded++;
			newGround.SetParent(this.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((Mathf.Abs(groundAdded * size.x) - Mathf.Abs(player.position.x)) <= distance) {
			Transform newGround = Instantiate (ground, new Vector3(groundAdded * size.x * 3.2f, 0, 0), Quaternion.identity) as Transform;
			newGround.name = "Ground#" + groundAdded;
			newGround.SetParent(this.transform);
			groundAdded++;
		}

		if ((Mathf.Abs(groundBack * size.x) - Mathf.Abs(player.position.x)) <= distance) {
			Transform newGround = Instantiate (ground, new Vector3(groundBack * size.x * 3.2f, 0, 0), Quaternion.identity) as Transform;
			newGround.name = "Ground#" + groundBack;
			newGround.SetParent(this.transform);
			groundBack--;
		}

		cleanupObjects ();
	}

	private void cleanupObjects() {
		foreach (Transform child in transform) {
			if (child == null || child.transform.gameObject == ground.gameObject)
				continue;
			if (Mathf.Abs (child.position.x - player.position.x) > distance) {
				if (player.position.x < child.position.x) {
					groundAdded--;
					Destroy (child.gameObject);
				} else {
					groundBack++;
					Destroy (child.gameObject);
				}
			}
		}
	}
}
