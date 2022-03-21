using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    // enemy helth 
    public int health;

    //explosion's gameobject
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameController gameController;
    void Start()
    {
        //getting explosion components
        gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
        this.GetComponent<DestroyByContact>().explosion = (GameObject)GameObject.Find("CartoonBlast_PSMedium");
        this.GetComponent<DestroyByContact>().playerExplosion = (GameObject)GameObject.Find("CartoonBlast_Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //enemy helth system 
        if (other.gameObject.tag.Equals("bullet"))
        {
            if (health < 0)
            {
                explosion.transform.position = this.transform.position;

                //play particle effect 
                explosion.GetComponent<ParticleSystem>().Play();
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

                //add score in player prefs 
                gameController.score += 10;
                gameController.scoreText.text = "Score: " + gameController.score.ToString();

                //play audio 
                FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(0);

            }

            else
            {
                health -= 1;
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            //player destroy or killed game over is true 
            FindObjectOfType<GameController>().GetComponent<GameController>().gameOver = true;
            explosion.transform.position = this.transform.position;

            //play particle effect of enemy and player if they contact with each other 
            explosion.GetComponent<ParticleSystem>().Play();
            this.gameObject.SetActive(false);
            playerExplosion.transform.position = other.transform.position;
            playerExplosion.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);

            //play audio source 
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(1);
            Invoke("GameOver", 2f);
        }
    }
    public void GameOver()
    {
        FindObjectOfType<GameController>().GetComponent<GameController>().GameOver();
    }
}
