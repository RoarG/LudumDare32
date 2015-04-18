using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

    public Renderer rend;
	// Use this for initialization
	void Start () {
        rend.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }

    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }
    
    void onMouseDown() {
        Application.LoadLevel("Level1");
    }
}
