using UnityEngine;
using System.Collections;

public class rotateChar : MonoBehaviour {

	// Use this for initialization
	void Start () {
       transform.rotation = Quaternion.Euler(0,180,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
