﻿using UnityEngine;
using System.Collections;

public class NPCVariables : MonoBehaviour {

	public bool randomWalk = true;
	public bool isAttacking = false;
	public double attackTimer = 1.0;
	public float projectileSpeed = 350.0f;
	public int health = 100; // variable from 0-100
	private int fatState = 1; // variable from 1-3?

	// Use this for initialization
	void Start () {
		for (int i = 1; i < 5; i++) {
			hideNPC (false, i);
		}
		hideNPC (true, fatState + 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (updateFatnessLevel ()) {
			for (int i = 1; i < 5; i++) {
				hideNPC (false, i);
			}
			hideNPC (true, fatState + 1);
		}
	}

	bool updateFatnessLevel(){
		bool change = false;
		// 100 - 67
		if (health > 66 && fatState != 1){
			fatState = 0;
			change = true;
			// 66 - 34
		} else if (health < 67 && health > 33 && fatState != 2){
			fatState = 1;
			change = true;
			// 33 - 1
		} else if (health < 34 && health > 0 && fatState != 3){
			fatState = 2;
			change = true;
		} else if (health < 1 && fatState != 0){
            Instantiate(GameObject.Find("flaske").transform, transform.position, Quaternion.identity);
            Destroy(gameObject);
		}
		return change;
	}

	private void hideNPC(bool hidden, int num){
		foreach (Transform child in transform) {
			if (child.name.Contains(""+num))
				child.gameObject.SetActive(hidden);
		}
	}
}
