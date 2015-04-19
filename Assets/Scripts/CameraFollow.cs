using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float distance = 3.0f;
	public static float height = 3.0f;
	public float dampening = 5.0f;
	public int changePos = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 wantedPosition = target.TransformPoint (new Vector3 (0, height, -distance));
		wantedPosition.x += changePos;
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * dampening);
	}
}
