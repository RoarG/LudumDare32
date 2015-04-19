using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public float cachedY;
    public RectTransform healthTransform;
    private float minXvalue;
    private float maxXvalue;
    private int currentHealth;

    PlayerVariables playerVar;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
            HandleHealth();
            playerVar.health = currentHealth;
            }
    }

    public float maxHealth;

    public float coolDown;
    private bool onCD;

    public Text healthText;
    public Image visualHealth;

    //Endrer fargen til barn når den er under denne.
    public int emptyAmmo = 20;

	// Use this for initialization
	void Start () {
        cachedY = healthTransform.position.y;
        minXvalue = healthTransform.position.x;
        maxXvalue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = (int) maxHealth;
        onCD = false;
        playerVar = GetComponent<PlayerVariables>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;

    }

    private void HandleHealth()
    {
        healthText.text = "Fat: " + currentHealth;

        float currentXvalue = MapValues(currentHealth, 0, maxHealth, minXvalue, maxXvalue);

        healthTransform.position = new Vector3(currentXvalue, cachedY);

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
        Debug.Log("Ontrigger: " + other);
        if (other.name == "Damage")
        {
            Debug.Log("Taking Damage onCD: " + onCD  + "  currentHealth: " + currentHealth);
            if (!onCD && currentHealth > 0)
            {
                Debug.Log("IF !oncd and currenHEalth");
                StartCoroutine(CoolDownDmg());
                CurrentHealth -= 1;
            }
        }
        if (other.name == "Health")
        {
            Debug.Log("Getting Healed" + onCD  + "  currentHealth: " + currentHealth);
            if (!onCD && currentHealth < maxHealth)
            {
                Debug.Log("IF !oncd and currenhealth less than maxhealth");
                StartCoroutine(CoolDownDmg());
                CurrentHealth += 1;
            }
        }
    }
}
