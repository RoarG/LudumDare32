using UnityEngine;
using System.Collections;

public class PlayerVariables : MonoBehaviour {

	public int health = 100; // variable from 0-100
	public static float weight = 100.0f; // variable from 0-100
	public int fatState = 3; // variable from 1-3?
	static bool poweredUp = false;
	static float lastPowerUpStart = 0;
	public bool isFat = true;

    public int madeFat;
    public int distance;
    public bool powerUpCD;

    HealthBar healthbar;


	// Use this for initialization
	void Start () {
		updateMass();
        healthbar = GetComponent<HealthBar>();
		transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isFat", isFat);

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





		if (Input.GetKeyDown(KeyCode.F)){
			health -= 10;
		}

		if (Input.GetKeyDown(KeyCode.G)){
			health += 10;
		}
		bool change = updateFatnessLevel();
		if (change){
			 changeState();
		}

        if (health <= 0)
        {
        //    PlayerPrefs.SetInt("Player Score", GameObject.Find("HealthBar").GetComponent<HealthBar>().currentMadeFat);
            Application.LoadLevel(2);
        }

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
        float hp = health;
        GetComponent<Rigidbody2D>().mass = 1.0f + (1 * (hp / 100));
	}

	public static void powerUp(){
		poweredUp = true;
		lastPowerUpStart = Time.time;
	}

	public bool isPoweredUp(){
		return poweredUp;
	}

	bool updateFatnessLevel(){
		bool change = false;
		// 100 - 67
		if (health > 66 && fatState != 3){
			fatState = 3;
			change = true;
		// 66 - 34
		} else if (health < 67 && health > 33 && fatState != 2){
			fatState = 2;
			change = true;
		// 33 - 1
		} else if (health < 34 && health > 0 && fatState != 1){
			fatState = 1;
			change = true;
		} else if (health < 1 && fatState != 0){
				fatState = 0;
				change = true;
			}
		return change;
	}

	void changeState(){
		int i = 0;
		while (i < 4){
			if (i == fatState){
				GameObject.Find("Player").GetComponent<PlayerMove>().hidePlayer(false, i);
			} else {
				GameObject.Find("Player").GetComponent<PlayerMove>().hidePlayer(true, i);
			}
            i++;
		}
	}


		
		
}
