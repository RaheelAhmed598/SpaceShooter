using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //spawn values of the objects 
    Vector3 spawnValues;
    private Vector2 screenBounds;
    Camera mainCamera;

    //point to get after destroy aestroid
    public int hazardCountt;

    //spawn wait and start time with wave 
    public float spawnWait;
    public float startWait;
    public float waveWait;

    //score display on the screen
    //high score at the end of the game 
    public Text scoreText;
    public Text highscore;
    public int score;

    //power up bulet game object prefab
    public GameObject powerUp;

    //game over screen
    public GameObject gameOverPanel;
   [HideInInspector] public bool gameOver = false;

    public void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        //setting the display of the high score using player prefs
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Invoke("Waves",1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Random.Range(0, 1000) == 28 && gameOver==false && FindObjectOfType<PlayerController>().GetComponent<PlayerController>().bullets_PowerUp == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, screenBounds.y + 0.3f);
            Instantiate(powerUp, spawnPosition, powerUp.transform.rotation);
        }
    }

    //wave spawn method 
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            //wave spawn method with aestroid using coroutine 
            for (int i = 0; i < hazardCountt; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, screenBounds.y + 0.3f);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject hazrad = this.GetComponent<ObjectPooler>().GetPooledObject();

                if (hazrad != null)
                {
                    hazrad.transform.position = spawnPosition;
                    hazrad.transform.rotation = spawnRotation;
                    hazrad.SetActive(true);
                }

                yield return new WaitForSeconds(spawnWait);
            }

            //wave spawn method with enemy using coroutine 
            Vector3 sPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, screenBounds.y + 0.3f);
            Quaternion sRotation = Quaternion.identity;

            GameObject enemy = this.GetComponent<EnemyPooler>().GetPooledObject();

            if (enemy != null)
            {
                enemy.transform.position = sPosition;
                enemy.transform.rotation = sRotation;
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    //Game over method 
    public void GameOver()
    {
        Time.timeScale = 0;
        //if game over set the panel true 
        gameOverPanel.SetActive(true);
        if (PlayerPrefs.GetInt("highscore") < score)
        {
            PlayerPrefs.SetInt("highscore", score);
          
        }

        //high score using player prefs
        highscore.text = "High score: " + PlayerPrefs.GetInt("highscore").ToString();
    }

    //replay button method waves and quit method 
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Waves()
    {
        StartCoroutine("SpawnWaves");
    }
}
