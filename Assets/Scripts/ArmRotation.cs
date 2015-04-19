using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset = 0;
	public float differenceX;
	static bool changeDirection = false;
	static string direction;
	float changeX = 0.5f;
	public float thePosX;


	// Update is called once per frame
	void Update () {

		if (changeDirection){
			Vector3 theScale = transform.localScale;
			theScale.y *= -1;
			transform.localScale = theScale;

			Vector3 thePosition = transform.position;

			if (direction == "Right"){
				thePosX = thePosition.x + changeX;
				thePosition.x = thePosX;
			} else if (direction == "Left"){
				thePosX = thePosition.x - changeX;
				thePosition.x = thePosX;
			}
			transform.position = thePosition;

			changeDirection = false;
		}

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 moveDirection = (mousePosition - transform.position).normalized;

		float rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		differenceX = mousePosition.x - transform.position.x;

		if (differenceX  > 0){
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
		}else if (differenceX < 0) {
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
		}


	
	}

	public static void changeDir(string dir){
		direction = dir;
		changeDirection = true;
	}
}
