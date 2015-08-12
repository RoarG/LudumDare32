using UnityEngine;
using System.Collections;

public class RandomWalk : MonoBehaviour {

	public float speed = 2.0f;
	private Vector3 startPos;
	private int dir = 1;
	private double length = 1.0f;
	private double aggroRange = 10.0;
	private double attackTimer = 0.0;

	private GameObject projectiles;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		projectiles = GameObject.Find("projectiles");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curPos = transform.position;
		if (this.GetComponent<NPCVariables> ().randomWalk) {
			if (Mathf.Sqrt (Mathf.Pow (curPos.x - startPos.x, 2)) < length) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * dir, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				length = Random.Range (0.0f, 10.0f);
				dir = Random.Range (0, 2);
				if (dir == 0)
					dir = -1;
				else
					dir = 1;
				startPos = curPos;
			}
			if (Mathf.Sqrt (Mathf.Pow (curPos.x - GameObject.Find("Player").transform.position.x, 2)) < aggroRange) {
				this.GetComponent<NPCVariables> ().isAttacking = true;
				this.GetComponent<NPCVariables> ().randomWalk = false;
			}
		} else if (this.GetComponent<NPCVariables> ().isAttacking) {
			float d = curPos.x - GameObject.Find("Player").transform.position.x;
			if (d < 0)
				dir = 1;
			else
				dir = -1;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * dir, GetComponent<Rigidbody2D> ().velocity.y);
			if (attackTimer < 0) {
				//attack
				
				Transform newProjectile = Instantiate (GameObject.Find ("carrot").transform, new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.identity) as Transform;
				if (dir < 0)
					newProjectile.localScale *= -1;
				Destroy (newProjectile.gameObject, 2.0f);
				newProjectile.transform.GetComponent<Rigidbody2D>().AddForce (new Vector2 (dir * GetComponent<NPCVariables> ().projectileSpeed, GetComponent<NPCVariables> ().projectileSpeed), ForceMode2D.Force);
				newProjectile.transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
				GetComponent<NPCVariables>().isAttacking = false;
				GetComponent<NPCVariables>().randomWalk = true;
				newProjectile.name = "projectile_carrot";
				newProjectile.SetParent(GameObject.Find ("projectiles").transform);

				attackTimer = GetComponent<NPCVariables> ().attackTimer;
			} else {
				attackTimer -= Time.deltaTime;
			}
			if (Mathf.Sqrt (Mathf.Pow (curPos.x - GameObject.Find("Player").transform.position.x, 2)) > aggroRange) {
				this.GetComponent<NPCVariables> ().isAttacking = false;
				this.GetComponent<NPCVariables> ().randomWalk = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Projectile_fat0")) {
			GetComponent<NPCVariables>().health -= 25;
			Destroy (coll.gameObject);
		}		
	}
}
