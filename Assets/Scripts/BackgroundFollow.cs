using UnityEngine;
using System.Collections;

public class BackgroundFollow : MonoBehaviour {

	public Vector3 offset;
	
	public Transform camera;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = camera.position + offset;
		newPos.z = offset.z;
		transform.position = newPos;
	}
}
