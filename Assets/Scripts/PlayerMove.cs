using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{


    private static int weight;

    public float speedForce = 10.0f;
    public static Vector2 jumpVector = new Vector2(0.0f, 350.0f);
    public float speed = 1.0f;
    
    public double cameraFollowPosX;
    public float differenceX;
    public static bool facingRight = true;
    public int posChange = 0;
    private float oldSpeed = 5.0f;
    private int playerPosX;

    public bool isShooting = false;
    public bool isMoving = false;
    public bool shootOnCD = false;
    private bool isGrounded;
    public bool isDead = false;

    public float length = 0.9f;
    public LayerMask ground;

    public float vol;
    public AudioClip magen;
    public AudioClip skudd;
    public AudioClip skuddLoop;

    private AudioSource source;

    // Offset of the camerafollow when character is turned. Used to avoid character spazing out when mouse is position over the middle of the character
    private double changeDirOffset = 0.21;



    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        weight = PlayerVariables.health;
        GetComponent<Rigidbody2D>().fixedAngle = true;
        hidePlayer(true, 0);
        hidePlayer(true, 1);
        hidePlayer(true, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate character
        playerPosX = (int)transform.position.x;
        GetComponent<HealthBar>().DistanceChange = playerPosX;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveDir = (mousePos - transform.Find("CameraFollow").position);
        cameraFollowPosX = transform.Find("CameraFollow").position.x;

        if (differenceX > changeDirOffset && !facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = true;
            ArmRotation.changeDir("Right");
            Vector3 oldPos = transform.Find("Karakter_3").position;
            oldPos.x -= posChange;
            transform.Find("Karakter_3").position = oldPos;
        }
        else if (differenceX < -changeDirOffset && facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = false;
            ArmRotation.changeDir("Left");
            Vector3 oldPos = transform.Find("Karakter_3").position;
            oldPos.x += posChange;
            transform.Find("Karakter_3").position = oldPos;
        }


        // Movement     
        weight = PlayerVariables.health;
        speed = (speedForce - ((speedForce - 1) * weight / 100));
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-(speedForce - (4 * weight / 100)), GetComponent<Rigidbody2D>().velocity.y);
            isMoving = true;
            transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_2").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_1").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_0").GetComponent<Animator>().SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedForce - (4 * weight / 100), GetComponent<Rigidbody2D>().velocity.y);
            isMoving = true;
            transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_2").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_1").GetComponent<Animator>().SetBool("isWalking", true);
            transform.Find("Karakter_0").GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            isMoving = false;
            transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isWalking", false);
            transform.Find("Karakter_2").GetComponent<Animator>().SetBool("isWalking", false);
            transform.Find("Karakter_1").GetComponent<Animator>().SetBool("isWalking", false);
            transform.Find("Karakter_0").GetComponent<Animator>().SetBool("isWalking", false);
        }

        // Deactivated
        /**
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            oldSpeed = speedForce;
            speedForce *= 2.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedForce = oldSpeed;
        }
        **/

        isGrounded = Physics2D.Linecast(this.transform.position, new Vector2(transform.position.x, transform.position.y - length), ground);// OverlapCircle (transform.position, radius, ground);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Force);
        }

        // Shooting
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!shootOnCD)
            {
                source.PlayOneShot(magen, vol);
                StopCoroutine(Fire());
                StartCoroutine(Fire());

            }
        }

        /** Moved this into the movement section 
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            isMoving = true;
            transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isWalking", true);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isMoving = false;
            transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isWalking", false);
        }
        **/

        differenceX = moveDir.x;
    }

    IEnumerator Fire()
    {
        shootOnCD = true;
        isShooting = true;
        transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isShooting", true);
        transform.Find("Karakter_2").GetComponent<Animator>().SetBool("isShooting", true);
        transform.Find("Karakter_1").GetComponent<Animator>().SetBool("isShooting", true);
        transform.Find("Karakter_0").GetComponent<Animator>().SetBool("isShooting", true);
        ShootProjectile.shoot();
        PlayerVariables.changeHealth(-1);
        yield return new WaitForSeconds(0.14f);
        source.PlayOneShot(skudd, vol);
        isShooting = false;
        transform.Find("Karakter_3").GetComponent<Animator>().SetBool("isShooting", false);
        transform.Find("Karakter_2").GetComponent<Animator>().SetBool("isShooting", false);
        transform.Find("Karakter_1").GetComponent<Animator>().SetBool("isShooting", false);
        transform.Find("Karakter_0").GetComponent<Animator>().SetBool("isShooting", false);
        shootOnCD = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PowerUp")
        {
            PlayerVariables.powerUp();
        }
    }

    public void hidePlayer(bool hidden, int player)
    {

        if (player == 0)
        {
            transform.Find("M_skyter").GetComponent<SpriteRenderer>().enabled = hidden;
        }
        foreach (Transform child in transform.Find("M_skyter"))
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().enabled = hidden;
            }
        }

        Transform character = GameObject.Find("Karakter_" + player).transform;
        Transform Rig = character.Find("KarakterRig").transform;
        foreach (Transform child in Rig.Find("M_tykk").transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().enabled = !hidden;
            }
        }
    }

}

