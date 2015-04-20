using UnityEngine;
using System.Collections;

public class EnemyTurn : MonoBehaviour {

	public bool facingRight;
	public float differenceX;
    public GameObject player;

	// Use this for initialization
	void Start () {
		Vector3 theScale = transform.Find ("Enemy").localScale;
		theScale.x *= -1;
		transform.Find ("Enemy").localScale = theScale;
		facingRight = false;
        player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 moveDir = (transform.position - player.transform.position).normalized;
		
		differenceX = moveDir.x;		
		
		if (differenceX > 0 && facingRight){
			Vector3 theScale = transform.Find ("Enemy").localScale;
			theScale.x *= -1;
			transform.Find ("Enemy").localScale = theScale;
			facingRight = false;
		} else if (differenceX < 0 && !facingRight){
			Vector3 theScale = transform.Find ("Enemy").localScale;
			theScale.x *= -1;
			transform.Find ("Enemy").localScale = theScale;
			facingRight = true;
		}
	}
}
