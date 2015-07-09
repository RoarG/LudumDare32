using UnityEngine;
using System.Collections;

public class rotateChar : MonoBehaviour {

	// Use this for initialization
	void Start () {

        InvokeRepeating("rotate", 0.2f, 0.02F);
        transform.rotation = Quaternion.Euler(0,180,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    void rotate() {
        
        transform.Rotate(0f, 0f, -5f);
        //Debug.Log("SPinning the wheels... carrots");
       
    }
}
