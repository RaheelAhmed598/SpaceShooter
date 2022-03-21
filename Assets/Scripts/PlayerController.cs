using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // physics material and speed
    Rigidbody rb;
    public float speed;
    Camera mainCamera;

    //player screen bound 
    private Vector2 screenBounds;
    float xMin, xMax, zMin, zMax;
    public float tilt;

    //powerup function with bullet
    public Text timeTextPowerUp;
    public bool bullets_PowerUp = false;
    private float time = 10.0f;

    //power up function of the bullet within 3 direction
    public Transform shotSpawn;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;
    public float fireRate;
    private float nextFire;

    //condition check
    private bool isMoving = false;
    public bool gameOver = false;

    Collider col;
    Vector3 pos;
    Camera cam;

    // Player health 
    int health = 3;

    //player explosion game object
    public GameObject playerExplosion;
    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        cam = Camera.main;
        mainCamera = Camera.main;

        //game screen bound
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x;
        xMax = screenBounds.x;
        zMin = -screenBounds.y / 4;
        zMax = screenBounds.y - (screenBounds.y / 4);
       

    }

    public void FixedUpdate()
    {
        if (isMoving)
        {
            //checking player movement condition
            rb.MovePosition(pos);
            rb.velocity = pos;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        //player movement in horizontal and vertical direction
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Clamp to restrict a value to a range that is defined by the min and max values.
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax),
        0.0f, Mathf.Clamp(rb.position.z, zMin, zMax));

        //Returns a rotation that rotates z degrees around the z axis,
        //x degrees around the x axis, and y degrees around the y axis; applied in that order.
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    }
    public void Update()
    {
        //bullets powerup
        if (bullets_PowerUp == true)
        {
            //bullet powerup time limit
            time -= Time.deltaTime * 1;
            timeTextPowerUp.text = time.ToString("f0");
            if (time <= 0)
            {
                bullets_PowerUp = false;
                timeTextPowerUp.text = "";
                time = 15.0f;
            }
        }

        
        if (!gameOver)
        {
            //using mouse button to move player and fire 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (col.Raycast(ray, out hit, 100.0f))
            {
                isMoving = Input.GetMouseButton(0);
            }
        }
        if (isMoving)
        {
            //player moving condtion is true 
            pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
            pos.z = cam.ScreenToWorldPoint(Input.mousePosition).z;

            //Fire
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                //powerup fire
                if (bullets_PowerUp)
                {
                    //fire shot using bject pooling rather than instantiate
                    GameObject bullet1 = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                    if (bullet1 != null)
                    {
                        bullet1.transform.position = shotSpawnLeft.transform.position;
                        bullet1.transform.rotation = shotSpawnLeft.transform.rotation;
                        bullet1.SetActive(true);
                    }
                    GameObject bullet2 = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                    if (bullet2 != null)
                    {
                        bullet2.transform.position = shotSpawnRight.transform.position;
                        bullet2.transform.rotation = shotSpawnRight.transform.rotation;
                        bullet2.SetActive(true);
                    }
                }
                GameObject bullet = gameObject.GetComponent<ObjectPooler>().GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = shotSpawn.transform.position;
                    bullet.transform.rotation = shotSpawn.transform.rotation;
                    bullet.SetActive(true);
                }

            }
        }


    }

    // using trigger on player 
    public void OnTriggerEnter(Collider other)
    {
        //condition to check when player collide with the enemy bullet 
       if( other.gameObject.tag.Equals("enemyBullet"))
        {
            //if player health less than 0
            if (health < 0)
            {
                //using game controller script game over is true.
                FindObjectOfType<GameController>().GetComponent<GameController>().gameOver = true;
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

                //explosion of the player position
                playerExplosion.transform.position = other.transform.position;

                //play paarticle effect using particle system
                playerExplosion.GetComponent<ParticleSystem>().Play();

                //play audio using audio manager
                FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(1);
                Invoke("GameOver", 2f);
            }
            else
            {
                other.gameObject.SetActive(false);
                health -= 1;
            }



        }

} 
    public void GameOver()
    {
        //initialize game script using game controller
        FindObjectOfType<GameController>().GetComponent<GameController>().GameOver();
    }

}
