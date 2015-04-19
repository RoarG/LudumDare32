using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset = 0;


	// Update is called once per frame
	void Update () {

		/**
		var mouse = Input.mousePosition;
		var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		var offset = new Vector2(mouse.x - screenPoint.x + 3.0f, mouse.y - screenPoint.y);
		var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
		*/
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 moveDirection = (mousePosition - transform.position).normalized;

		float rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


		/**
		if (!PlayerMove.facingRight){
			rotationOffset = 180;
		} else {
			rotationOffset = 0;
		}

		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition - transform.position);
		difference.Normalize ();
		
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
		*/
	
	}
}
