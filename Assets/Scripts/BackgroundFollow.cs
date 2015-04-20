using UnityEngine;
using System.Collections;

public class BackgroundFollow : MonoBehaviour {

	public Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);

	public float distance = 120.0f;

	private Transform camera;
	private Transform player;

	private GameObject sky;
	private GameObject horizon;
	private GameObject mountains;
	private GameObject ground_behind;
	private GameObject ground_middle;
	private GameObject ground_front;

	private GameObject clouds;

	private Vector3 oldPos;

	// Use this for initialization
	void Start () {
		clouds = transform.FindChild ("Clouds").gameObject;
		camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
		oldPos = camera.transform.position;
		player = GameObject.FindGameObjectWithTag("Player").transform;

		sky = transform.FindChild ("sky").gameObject;

		horizon = transform.FindChild ("horizon").gameObject;
		for (int i = -3; i < 3; i++) {
			Transform newHorizon = Instantiate (horizon.transform, new Vector3(i * 40, offset.y, 4.0f), Quaternion.identity) as Transform;
			newHorizon.name = "horizon#" + newHorizon.position.x;
			newHorizon.SetParent(this.transform);
		}

		mountains = transform.FindChild ("mountains").gameObject;
		for (int i = -3; i < 3; i++) {
			Transform newMountain = Instantiate (mountains.transform, new Vector3(i * 40, offset.y, 3.0f), Quaternion.identity) as Transform;
			newMountain.name = "mountains#" + newMountain.position.x;
			newMountain.SetParent(this.transform);
		}

		ground_behind = transform.FindChild ("ground_behind").gameObject;
		for (int i = -3; i < 3; i++) {
			Transform newGround_behind = Instantiate (ground_behind.transform, new Vector3(i * 40, offset.y, 2.0f), Quaternion.identity) as Transform;
			newGround_behind.name = "ground_behind#" + newGround_behind.position.x;
			newGround_behind.SetParent(this.transform);
		}

		ground_middle = transform.FindChild ("ground_middle").gameObject;
		for (int i = -3; i < 3; i++) {
			Transform newGround_middle = Instantiate (ground_middle.transform, new Vector3(i * 40, offset.y - 2.5f, 1.5f), Quaternion.identity) as Transform;
			newGround_middle.name = "ground_middle#" + newGround_middle.position.x;
			newGround_middle.SetParent(this.transform);
		}

		ground_front = transform.FindChild ("ground_front").gameObject;
		for (int i = -3; i < 3; i++) {
			Transform newGround_front = Instantiate (ground_front.transform, new Vector3(i * 40, offset.y + 1.5f, 1.0f), Quaternion.identity) as Transform;
			newGround_front.name = "ground_front#" + newGround_front.position.x;
			newGround_front.SetParent(this.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camPos = camera.position;
		Vector3 deltaPos = camPos - oldPos;

		sky.transform.position = new Vector3((camPos + offset).x, (camPos + offset).y - CameraFollow.height, 5.0f);

		float frontPosX = -2147483647;
		float backPosX = 2147483647;
		Transform front = null;
		Transform back = null;
		foreach (GameObject h in GameObject.FindGameObjectsWithTag("Horizon")) {
			if (h == horizon)
				continue;
			h.transform.position += new Vector3(deltaPos.x / 1.1f, deltaPos.y, 0.0f);
			if (h.transform.position.x > frontPosX) {
				frontPosX = h.transform.position.x;
				front = h.transform;
			}
			if (h.transform.position.x < backPosX) {
				backPosX = h.transform.position.x;
				back = h.transform;
			}
		}

		if (Mathf.Sqrt(Mathf.Pow(front.position.x - player.position.x, 2)) <= distance) {
			Transform newHorizon = Instantiate (horizon.transform, new Vector3(front.position.x + 40, (camPos + offset).y - CameraFollow.height, 4.0f), Quaternion.identity) as Transform;
			newHorizon.name = "horizon#" + front.position.x;
			newHorizon.SetParent(this.transform);
		}
		
		if (Mathf.Sqrt(Mathf.Pow(player.position.x - (back.position.x - 40), 2)) <= distance) {
			Transform newHorizon = Instantiate (horizon.transform, new Vector3(back.position.x - 40, (camPos + offset).y - CameraFollow.height, 4.0f), Quaternion.identity) as Transform;
			newHorizon.name = "horizon#" + back.position.x;
			newHorizon.SetParent(this.transform);
		}

		frontPosX = -2147483647;
		backPosX = 2147483647;
		front = null;
		back = null;
		foreach (GameObject m in GameObject.FindGameObjectsWithTag("Mountains")) {
			if (m == mountains)
				continue;
			m.transform.position += new Vector3(deltaPos.x / 1.2f, deltaPos.y, 0.0f);
			if (m.transform.position.x > frontPosX) {
				frontPosX = m.transform.position.x;
				front = m.transform;
			}
			if (m.transform.position.x < backPosX) {
				backPosX = m.transform.position.x;
				back = m.transform;
			}
		}
		
		if (Mathf.Sqrt(Mathf.Pow(front.position.x - player.position.x, 2)) <= distance) {
			Transform newMountains = Instantiate (mountains.transform, new Vector3(front.position.x + 40, (camPos + offset).y - CameraFollow.height, 3.0f), Quaternion.identity) as Transform;
			newMountains.name = "mountains#" + front.position.x;
			newMountains.SetParent(this.transform);
		}
		
		if (Mathf.Sqrt(Mathf.Pow(player.position.x - (back.position.x - 40), 2)) <= distance) {
			Transform newMountains = Instantiate (mountains.transform, new Vector3(back.position.x - 40, (camPos + offset).y - CameraFollow.height, 3.0f), Quaternion.identity) as Transform;
			newMountains.name = "mountains#" + back.position.x;
			newMountains.SetParent(this.transform);
		}

		frontPosX = -2147483647;
		backPosX = 2147483647;
		front = null;
		back = null;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ground_behind")) {
			if (g == ground_behind)
				continue;
			g.transform.position += new Vector3(deltaPos.x / 1.3f, deltaPos.y, 0.0f);
			if (g.transform.position.x > frontPosX) {
				frontPosX = g.transform.position.x;
				front = g.transform;
			}
			if (g.transform.position.x < backPosX) {
				backPosX = g.transform.position.x;
				back = g.transform;
			}
		}
		
		if (Mathf.Sqrt(Mathf.Pow(front.position.x - player.position.x, 2)) <= distance) {
			Transform newGround_behind = Instantiate (ground_behind.transform, new Vector3(front.position.x + 40, front.position.y, front.position.z), Quaternion.identity) as Transform;
			newGround_behind.name = "ground_behind#" + front.position.x;
			newGround_behind.SetParent(this.transform);
		}
		
		if (Mathf.Sqrt(Mathf.Pow(player.position.x - (back.position.x - 40), 2)) <= distance) {
			Transform newGround_behind = Instantiate (ground_behind.transform, new Vector3(back.position.x - 40, front.position.y, front.position.z), Quaternion.identity) as Transform;
			newGround_behind.name = "ground_behind#" + back.position.x;
			newGround_behind.SetParent(this.transform);
		}

		frontPosX = -2147483647;
		backPosX = 2147483647;
		front = null;
		back = null;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ground_middle")) {
			if (g == ground_middle)
				continue;
			g.transform.position += new Vector3(deltaPos.y / 1.4f, deltaPos.y / 1.5f, 0.0f);
			if (g.transform.position.x > frontPosX) {
				frontPosX = g.transform.position.x;
				front = g.transform;
			}
			if (g.transform.position.x < backPosX) {
				backPosX = g.transform.position.x;
				back = g.transform;
			}
		}
		
		if (Mathf.Sqrt(Mathf.Pow(front.position.x - player.position.x, 2)) <= distance) {
			Transform newGround_middle = Instantiate (ground_middle.transform, new Vector3(front.position.x + 40, front.position.y, front.position.z), Quaternion.identity) as Transform;
			newGround_middle.name = "ground_middle#" + front.position.x;
			newGround_middle.SetParent(this.transform);
		}
		
		if (Mathf.Sqrt(Mathf.Pow(player.position.x - (back.position.x - 40), 2)) <= distance) {
			Transform newGround_middle = Instantiate (ground_middle.transform, new Vector3(back.position.x - 40, front.position.y, front.position.z), Quaternion.identity) as Transform;
			newGround_middle.name = "ground_middle#" + back.position.x;
			newGround_middle.SetParent(this.transform);
		}

		frontPosX = -2147483647;
		backPosX = 2147483647;
		front = null;
		back = null;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Ground_front")) {
			if (g == ground_front)
				continue;
			if (g.transform.position.x > frontPosX) {
				frontPosX = g.transform.position.x;
				front = g.transform;
			}
			if (g.transform.position.x < backPosX) {
				backPosX = g.transform.position.x;
				back = g.transform;
			}
		}
		
		if (Mathf.Sqrt(Mathf.Pow(front.position.x - player.position.x, 2)) <= distance) {
			Transform newGround_front = Instantiate (ground_front.transform, new Vector3(front.position.x + 40, offset.y + 1.5f, 1.0f), Quaternion.identity) as Transform;
			newGround_front.name = "ground_front#" + front.position.x;
			newGround_front.SetParent(this.transform);
		}
		
		if (Mathf.Sqrt(Mathf.Pow(player.position.x - (back.position.x - 40), 2)) <= distance) {
			Transform newGround_front = Instantiate (ground_front.transform, new Vector3(back.position.x - 40, offset.y + 1.5f, 1.0f), Quaternion.identity) as Transform;
			newGround_front.name = "ground_front#" + back.position.x;
			newGround_front.SetParent(this.transform);
		}

		cleanupObjects ();
		oldPos = camPos;
	}

	private void cleanupObjects() {
		foreach (Transform child in transform) {
			if (child == null)
				continue;
			if (child.tag.Equals("Horizon")) {
				if (child == horizon.transform)
					continue;
				if (Mathf.Sqrt (Mathf.Pow(child.position.x - player.position.x, 2)) > distance) {
					Destroy (child.gameObject);
				}
			}
			if (child.tag.Equals("Mountains")) {
				if (child == mountains.transform)
					continue;
				if (Mathf.Sqrt (Mathf.Pow(child.position.x - player.position.x, 2)) > distance) {
					Destroy (child.gameObject);
				}
			}
			if (child.tag.Equals("Ground_behind")) {
				if (child == ground_behind.transform)
					continue;
				if (Mathf.Sqrt (Mathf.Pow(child.position.x - player.position.x, 2)) > distance) {
					Destroy (child.gameObject);
				}
			}
			if (child.tag.Equals("Ground_middle")) {
				if (child == ground_middle.transform)
					continue;
				if (Mathf.Sqrt (Mathf.Pow(child.position.x - player.position.x, 2)) > distance) {
					Destroy (child.gameObject);
				}
			}
			if (child.tag.Equals("Ground_front")) {
				if (child == ground_front.transform)
					continue;
				if (Mathf.Sqrt (Mathf.Pow(child.position.x - player.position.x, 2)) > distance) {
					Destroy (child.gameObject);
				}
			}
		}
	}
}
