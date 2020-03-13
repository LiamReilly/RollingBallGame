using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject Player;
    public GameObject Player2;
    public Text timer;
    public float seconds;
    public int minutes;
    public Text winText;
    public bool gameActive;
    Scene currentScene;
    string scenename;
    public int cubeCount;
    public Text timeWarning;
    public GameObject ControlOptions;
    public bool usingMouse;
    public GameObject levelMusic;
    private AudioSource source1;
    

    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        scenename = currentScene.name;
        timer.text = "";
        panel.SetActive(true);
        timeWarning.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        gameActive = false;
        ControlOptions.gameObject.SetActive(false);
        if(scenename == "2PlayerArea")
        {
            ControlOptions.gameObject.SetActive(true);
        }
        source1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            seconds -= Time.deltaTime;
            timer.text = minutes + ":" + seconds;
        }
        if (seconds <10 && seconds > 0)
        {
            timer.text = minutes + ":0" + seconds;
        }
        if (minutes == 0 && !timeWarning.gameObject.activeSelf)
        {   if (seconds > 57f)
            {
                timeWarning.gameObject.SetActive(true);
                StartCoroutine(wait3Seconds());
            }

        }
        if (seconds < 10f && minutes == 0)
        {
            timeWarning.text = "Time Almost Up!!!!";
            timeWarning.gameObject.SetActive(true);
        }
        if(seconds < 7f && minutes == 0)
        {
            timeWarning.gameObject.SetActive(false);
        }
        if (seconds <= 0)
            {
                minutes--;
            seconds = 60f;
                //timer.text = minutes + ":" + seconds;
                if (minutes < 0)
                {
                    minutes = 0;
                    seconds = 0;
                    Player.gameObject.GetComponent<PlayerController>().GameOver = true;
                if (scenename == "MinigameArea")
                {
                    winText.text = "You Lose!!!!";
                }
                
                
                    if(Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score == 0)
                    {
                        winText.text = "It's A Tie!!!!";
                    }
                    if (Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score > 0)
                    {
                        winText.text = "Player 1 Wins!!!!";
                    }
                    if (Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score < 0)
                    {
                        winText.text = "Player 2 Wins!!!!";
                    }
                    winText.gameObject.SetActive(true);
                    gameActive = false;
                
                }
            
        }
        if (cubeCount <= 0)
        {
            if (Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score == 0)
            {
                winText.text = "It's A Tie!!!!";
            }
            if (Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score > 0)
            {
                winText.text = "Player 1 Wins!!!!";
            }
            if (Player.gameObject.GetComponent<PlayerController>().score - Player2.gameObject.GetComponent<Player2Controller>().score < 0)
            {
                winText.text = "Player 2 Wins!!!!";
            }
            winText.gameObject.SetActive(true);
            gameActive = false;
            Player.gameObject.GetComponent<PlayerController>().GameOver=true;
            Player2.gameObject.GetComponent<Player2Controller>().GameOver = true;
            levelMusic.gameObject.SetActive(false);



        }
    }

    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        timeWarning.gameObject.SetActive(false);
    }
    public void start()
    {
        source1.Play();
        Player.gameObject.GetComponent<PlayerController>().GameOver = false;
        panel.SetActive(false);
        timer.gameObject.SetActive(true);
        gameActive = true;
        levelMusic.gameObject.SetActive(true);
        
    }
    public void quit()
    {
        source1.Play();
        Application.Quit();
    }
    public void Start2Player()
    {
        source1.Play();
        SceneManager.LoadScene("2PlayerArea");
        

    }
    void twoPlayerSetup()
    {
        timer.gameObject.SetActive(true);
        gameActive = true;
        levelMusic.gameObject.SetActive(true);
    }
    public void mouseOption()
    {
        source1.Play();
        twoPlayerSetup();
        ControlOptions.gameObject.SetActive(false);
        usingMouse = true;
        Player2.gameObject.GetComponent<Player2Controller>().usingMouse = true;
    }
    public void arrowOption()
    {
        source1.Play();
        twoPlayerSetup();
        ControlOptions.gameObject.SetActive(false);
        usingMouse = false;
        Player2.gameObject.GetComponent<Player2Controller>().usingMouse = false;
    }
    public void resetGame()
    {
        source1.Play();
        SceneManager.LoadScene("MinigameArea");
    }
}
