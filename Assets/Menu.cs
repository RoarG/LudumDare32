using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("TEST");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("ONDOWN");
        Application.LoadLevel(1);
        PlayerPrefs.DeleteAll();
        PlayerVariables.health = 100;
        
    }
}
