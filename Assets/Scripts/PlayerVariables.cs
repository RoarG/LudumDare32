using UnityEngine;
using System.Collections;

public class PlayerVariables : MonoBehaviour {

	public int health = 100; // variable from 0-100
	public static float weight = 100.0f; // variable from 0-100
	public int fatnessLevel; // variable from 1-3?
	static bool poweredUp = false;
	static float lastPowerUpStart = 0;
    public int madeFat;
    public int distance;


    HealthBar healthbar;


	// Use this for initialization
	void Start () {
		updateMass();
        healthbar = GetComponent<HealthBar>();

	}
	
	// Update is called once per frame
	void Update () {


		changeWeight (-0.1f);
		updateMass ();

		// implement powerUp function here
		if (isPoweredUp()) {
			// jumpVector changed only for testing purposes
			PlayerMove.jumpVector = new Vector2(0.0f, 800.0f);
		}

		// deactivate powerUpFunction here
		if (isPoweredUp() && (Time.time - lastPowerUpStart) > 5.0f) {
			poweredUp = false;
			// jumpVector changed only for testing purposes
			PlayerMove.jumpVector = new Vector2(0.0f, 400.0f);
		}

        healthbar.DistanceChange = ((int) transform.position.x) / 10;

	}
	
	public void changeHealth(int change){
		health = health + change;
	}
	
	public void changeWeight(float change){
		weight = weight + change;
		if (weight < 1) {
			weight = 1;
		}
	}

	void updateMass(){
		GetComponent<Rigidbody2D> ().mass = 1.0f+(2*(weight/100));
	}

	public static void powerUp(){
		poweredUp = true;
		lastPowerUpStart = Time.time;
	}

	public bool isPoweredUp(){
		return poweredUp;
	}


		
		
}
