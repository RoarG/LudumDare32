﻿using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {


	private float weight;

	public float speedForce = 10.0f;
	public static Vector2 jumpVector = new Vector2(0.0f, 400.0f);
	public float speed = 1.0f;
	public float differenceX;
	public static bool facingRight = true;
	public int posChange = 3;
	private float oldSpeed = 5.0f;



	private bool isGrounded;

	public float length = 0.6f;
	public LayerMask ground;

	// Use this for initialization
	void Start () {
		weight = PlayerVariables.weight;
		GetComponent<Rigidbody2D>().fixedAngle = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 moveDir = (mousePos - transform.Find("CameraFollow").position).normalized;
		
		differenceX = moveDir.x;


		if (differenceX > 0 && !facingRight){
			Vector3 theScale = transform.Find ("Char_tykk").localScale;
			theScale.x *= -1;
			transform.Find ("Char_tykk").localScale = theScale;
			facingRight = true;
			/**Vector3 oldPos = transform.Find("Char_tykk").position;
			Vector2 oldOffset =  GetComponent<BoxCollider2D>().offset;
			oldOffset.x += 3;
			GetComponent<BoxCollider2D>().offset = oldOffset;
			*/
			ArmRotation.changeDir("Right");
			Vector3 oldPos = transform.Find("Char_tykk").position;
			oldPos.x -= posChange;
			transform.Find("Char_tykk").position = oldPos;
		} else if (differenceX < 0 && facingRight){
			Vector3 theScale = transform.Find ("Char_tykk").localScale;
			theScale.x *= -1;
			transform.Find("Char_tykk").localScale = theScale;
			facingRight = false;
			/**
			Vector2 oldOffset = GetComponent<BoxCollider2D>().offset;
			oldOffset.x -= 3;
			GetComponent<BoxCollider2D>().offset = oldOffset;
			*/
			ArmRotation.changeDir("Left");
			Vector3 oldPos = transform.Find("Char_tykk").position;
			oldPos.x += posChange;
			transform.Find("Char_tykk").position = oldPos;
		}


		

		weight = PlayerVariables.weight;
		speed = (speedForce - ((speedForce-1) * weight / 100));
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-(speedForce - (4*weight/100)), GetComponent<Rigidbody2D>().velocity.y);
		} else if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speedForce - (4*weight/100), GetComponent<Rigidbody2D>().velocity.y);
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			oldSpeed = speedForce;
			speedForce *= 10.0f;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speedForce = oldSpeed;
		}

		isGrounded = Physics2D.Linecast (this.transform.position, new Vector2 (transform.position.x, transform.position.y - length), ground);// OverlapCircle (transform.position, radius, ground);

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			GetComponent<Rigidbody2D>().AddForce (jumpVector, ForceMode2D.Force);
		}

		if (Input.GetKey (KeyCode.Mouse0)) {
			transform.Find ("Char_tykk").GetComponent<Animator>().SetBool("isShooting", true);
			//WaitForSeconds(0.1);
			transform.Find ("Char_tykk").GetComponent<Animator>().SetBool("isShooting", false);
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "PowerUp") {
			PlayerVariables.powerUp ();
		}		
	}

	float setMass() {
		return 1.0f + (1 * (weight / 100));
	}

	


}
