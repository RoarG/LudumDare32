using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float speedForce = 5.0f;
	public Vector2 jumpVector = new Vector2(0.0f, 300.0f);

	private bool isGrounded;

	public float length = 0.6f;
	public LayerMask ground;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().fixedAngle = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-speedForce, GetComponent<Rigidbody2D>().velocity.y);
		} else if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speedForce, GetComponent<Rigidbody2D>().velocity.y);
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
		}

		isGrounded = Physics2D.Linecast (this.transform.position, new Vector2 (transform.position.x, transform.position.y - length), ground);// OverlapCircle (transform.position, radius, ground);

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			GetComponent<Rigidbody2D>().AddForce (jumpVector, ForceMode2D.Force);
		}
	}
}
