using UnityEngine;
using System.Collections;

public class scoreDestroy : MonoBehaviour {


    HealthBar healthbar;
  
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Ontrigger: " + other.tag);
        Debug.Log("Ontrigger name: " + other.name);

        if (other.name == "Enemy")
        {
            Debug.Log("GSDFASDF");
//            healthbar.CurrentmadeFat += 1;
            StartCoroutine(destroy(other.transform.gameObject));
            
        }

    }
    IEnumerator destroy(GameObject ob){
        yield return new WaitForSeconds(0.1f);
        //Destroy(ob);
    }

    
}
