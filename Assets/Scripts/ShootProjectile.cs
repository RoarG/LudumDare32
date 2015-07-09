using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	private static GameObject projectileFat;
	private static GameObject player;

    public float vol;
    public AudioClip magen;
    public AudioClip skudd;
    public AudioClip skuddLoop;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("CameraFollow");
		projectileFat = GameObject.FindGameObjectWithTag ("Projectile_fat0");
	}
	
	// Update is called once per frame
    /**
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
            source.PlayOneShot(magen, vol);
			Vector3 dir = Input.mousePosition;
			dir.z = 0;
			dir = Camera.main.ScreenToWorldPoint(dir) - new Vector3(player.transform.position.x, player.transform.position.y, 0);
			dir.Normalize();

			Transform newProjectile = Instantiate (projectileFat.transform, new Vector3(player.transform.position.x, player.transform.position.y, -1.0f), Quaternion.identity) as Transform;
			if (dir.x < 0)
				newProjectile.localScale *= -1;
			Destroy (newProjectile.gameObject, 2.0f);
			newProjectile.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2 (dir.x * 1000, dir.y * 1000));
			newProjectile.name = "projectile_fat0";
			newProjectile.SetParent(this.transform);
			//newProjectile.tag = "projectile";

		}
	}
    **/

    public static void shoot()
    {
        Vector3 dir = Input.mousePosition;
        dir.z = 0;
        dir = Camera.main.ScreenToWorldPoint(dir) - new Vector3(player.transform.position.x, player.transform.position.y, 0);
        dir.Normalize();

        Transform newProjectile = Instantiate(projectileFat.transform, new Vector3(player.transform.position.x, (player.transform.position.y), -1.0f), Quaternion.identity) as Transform;
        if (dir.x < 0)
            newProjectile.localScale *= -1;
        Destroy(newProjectile.gameObject, 2.0f);
        newProjectile.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * 1000, dir.y * 1000));
        newProjectile.name = "projectile_fat0";
        // newProjectile.SetParent(this.transform);
    }
}
