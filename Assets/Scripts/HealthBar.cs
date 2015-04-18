using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public float cachedY;
    public RectTransform healthTransform;
    private float minXvalue;
    private float maxXvalue;
    private int currentHealth;

    private int CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
            HandleHealth();
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

        if (currentHealth > emptyAmmo)
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, emptyAmmo, maxHealth, 255, 0), 255, 0, 255);
        }
        else
        {
            visualHealth.color = new Color32(255,(byte)MapValues(currentHealth, 0, emptyAmmo, 0, 255), 0, 255);
        }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; 
    }
}
