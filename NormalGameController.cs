using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalGameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject background;
    public GameObject starfield;

    public Vector3 spawnValues;
    public AudioSource _AudioSource1;
    public AudioSource _AudioSource2;
    public AudioSource _AudioSource3;
    public int hazardCount;
    public int points;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    
    public Text pointsText;
    public Text restartText;
    public Text escapeText;
    public Text menuText;
    public Text gameOverText;


    public bool gameOver;
    private bool restart;

    BGScroller refScript1;
    BGScroller2 refScript2;


    void Start()
    {
        _AudioSource1.Play();
        gameOver = false;
        restart = false;
        restartText.text = "";
        menuText.text = "";
        escapeText.text = "";
        gameOverText.text = "";
        points = 0;
        UpdatePoints();
        StartCoroutine(SpawnWaves());
        refScript1 = GetComponent<BGScroller>();
        refScript2 = GetComponent<BGScroller2>();

    }

    void Update()
    {
      
        pointsText.text = "Points:" + points;
        if (points >= 100)
        {
            gameOverText.text = "GAME CREATED BY CAITLIN CONRAD";
            gameOver = true;
            restart = true;
            background.GetComponent<BGScroller>().scrollSpeed = -10f;
            starfield.GetComponent<BGScroller2>().scrollSpeed = -5f;

            if (_AudioSource1.isPlaying)
            {
                _AudioSource1.Stop();

                _AudioSource2.Play();
            }

        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("Normal mode");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


            if (gameOver)
            {

                escapeText.text = "Press 'ESC' to Quit Game";
                menuText.text = "Press 'M' for Main Menu";
                restartText.text = "Press 'S' for Restart";
                restart = true;
                break;

            }
        }


    }

    public void AddScore(int newScoreValue)
    {
        points += newScoreValue;
        UpdatePoints();
    }

    void UpdatePoints()
    {
        pointsText.text = "Points: " + points;
    }

    public void GameOver()
    {

        gameOverText.text = "Game Over!";
        gameOver = true;

        if (_AudioSource1.isPlaying)
        {
            _AudioSource1.Stop();

            _AudioSource3.Play();
        }
    }

}

