using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public float cachedY;
    public RectTransform healthTransform;
    private float minXvalue;
    private float maxXvalue;
    public float maxHealth;
    public int currentHealth;
    public int currentMadeFat;
    public int currentDist;

    public float coolDown;
    private bool onCD;

    public Text healthText;
    public Image visualHealth;
    PlayerVariables playerVar;

    public Text MadeFat;
    public Text Distance;

    //Endrer fargen til barn når den er under denne.
    public int emptyAmmo = 20;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
            HandleHealth();
            playerVar.health = currentHealth;
            }
    }

    public int CurrentmadeFat
    {
        get { return currentMadeFat; }
        set
        {
            currentMadeFat = value;
            HandleFat();
            playerVar.madeFat = currentMadeFat;
        }
    }

    public int DistanceChange
    {
        get { return currentMadeFat; }
        set
        {
            currentDist = value;
            HandleDistance();
            playerVar.distance = currentDist;
        }
    }

    private void HandleFat()
    {
        MadeFat.text = "FatScore: " + currentMadeFat;
  
    }




	// Use this for initialization
	void Start () {
        cachedY = healthTransform.position.y;
        minXvalue = 0;
        maxXvalue = healthTransform.localScale.x;
        currentHealth = (int) maxHealth;
        onCD = false;
        playerVar = GetComponent<PlayerVariables>();


    //    healthTransform
     //   healthText
     //   visualHealth = GameObject.Find("Health").GetComponent<Image>;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void HandleDistance()
    {
        Distance.text = "Distance : " + playerVar.distance;
        Debug.Log("TEST");
       
    }

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;

    }

    private void HandleHealth()
    {
        healthText.text = currentHealth + "% Fat";

        float currentXvalue = MapValues(currentHealth, 0, maxHealth, minXvalue, maxXvalue);
        Debug.Log("Current X value: " + currentXvalue + "  healthTransform: " + healthTransform.position);

        healthTransform.localScale = new Vector3(currentXvalue, cachedY);

        Debug.Log("Current X value: " + currentXvalue + "  healthTransform: " + healthTransform.position);

        if (currentHealth > emptyAmmo) //More than emptyAmmo
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, emptyAmmo, maxHealth, 255, 0), 255, 0, 255);
        }
        else //less then emptyAmmo
        {
            visualHealth.color = new Color32(255,(byte)MapValues(currentHealth, 0, emptyAmmo, 0, 255), 0, 255);
        }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; 
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("Ontrigger: " + other.tag);
        if (other.tag == "Damage")
        {
            Debug.Log("Taking Damage onCD: " + onCD  + "  currentHealth: " + currentHealth);
            if (!onCD && currentHealth > 0)
            {
                Debug.Log("IF !oncd and currenHEalth");
                StartCoroutine(CoolDownDmg());
                CurrentHealth -= 1;
            }
        }
        if (other.tag == "food")
        {
            Debug.Log("Getting Healed" + onCD  + "  currentHealth: " + currentHealth);
            if (!onCD && currentHealth < maxHealth)
            {
                Debug.Log("IF !oncd and currenhealth less than maxhealth: " + other.transform.gameObject);
                StartCoroutine(CoolDownDmg());
                CurrentHealth += 10;
                Destroy(other.transform.gameObject);
            }
        }
    }
}
