using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody rb;
     public float thrust;
     public float jumpPower;
     public int score;
     public Text countText;
     public Text winText;
     public int NumberOfCubes;
     public bool GameOver;
     public GameObject GameManager;
    public GameObject Player;
    public Text collisionText;
    public PlayerController playerScript;
   public  bool usingMouse;
    public int mouseThrust;
    private Vector3 targetPosition;
    public AudioSource pickUpSound;
    public AudioSource jumpSound;
    public AudioSource badCollide;


    //private Vector3 screenPoint; //part of new mouse movement method
    //private Vector3 offset;




    private void Start()
    {
         rb = GetComponent<Rigidbody>();          
         score = 0;
         setCountText();
         winText.gameObject.SetActive(false);
         GameOver = false;
        targetPosition = transform.position;


    }
    void update()
    {
        

    }

    void FixedUpdate()
    {
        if (GameOver == false& usingMouse != true)
        {
            /*float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            addThrust(movement);
            */
            if (Input.GetKey(KeyCode.LeftArrow))
            {
               rb.AddForce ((-1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce((1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(0, 0, (1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(0, 0, (-1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.RightShift) && transform.position.y == 0.5f)
            {
                jumpSound.Play();
                rb.AddForce(0, jumpPower, 0);
            }

        }
        //var d = Input.GetAxis("Mouse ScrollWheel");
        
        if (GameOver == false & usingMouse != false)
        {
            //meme mouse movement
            /* if (Input.GetMouseButton(1))
             {
                 rb.AddForce(1 * mouseThrust, 0, 0);
             }
             if (Input.GetMouseButton(0))
             {
                 rb.AddForce(-1 * mouseThrust, 0, 0);
             }
             if (d > 0f)
             {
                 rb.AddForce(0, 0, 4 * mouseThrust);
             }else
             if(d<0f)
             {
                 rb.AddForce(0, 0, -4 * mouseThrust);
             }
             if(Input.GetMouseButton(1)&& Input.GetMouseButton(0) && transform.position.y == 0.5f)
             {
                 rb.AddForce(0, jumpPower, 0);
             }*/

            //failed mouse movement
            /*screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position; 
            offset = offset - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            rb.AddForce(screenPoint); */
            
                var horMove = Input.GetAxis("Mouse X");
                var vertMove = Input.GetAxis("Mouse Y");
                rb.AddForce(new Vector3(horMove*mouseThrust, 0, vertMove*mouseThrust));
            if (Input.GetMouseButton(0) && transform.position.y == 0.5f)
            {
                
                rb.AddForce(0, jumpPower, 0); 
                    jumpSound.Play();
                
            }


        }
            if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("MinigameArea");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") && GameOver != true)
        {
            other.gameObject.SetActive(false);
            score++;
            pickUpSound.Play();
            setCountText();
            GameManager.gameObject.GetComponent<GameManager>().cubeCount--;
            /* NumberOfCubes--;
             Player.gameObject.GetComponent<PlayerController>().NumberOfCubes--;

             if (NumberOfCubes <= 0)
             {
                 if (SceneManager.GetActiveScene().name != "MinigameArea")
                 {
                     if (score - Player.gameObject.GetComponent<Player2Controller>().score == 0)
                     {
                         winText.text = "It's A Tie!!!!";
                     }
                     if (score - Player.gameObject.GetComponent<Player2Controller>().score > 0)
                     {
                         winText.text = "Player 2 Wins!!!!";
                     }
                     if (score - Player.gameObject.GetComponent<Player2Controller>().score < 0)
                     {
                         winText.text = "Player 1 Wins!!!!";
                     }
                 }
                 winText.gameObject.SetActive(true);
                 GameOver = true;
                 GameManager.gameObject.GetComponent<GameManager>().gameActive = false;
             }*/

        }
        if (other.gameObject.CompareTag("wall")) {
            score--;
            badCollide.Play();


            if (GameOver != true)
            {
                setCountText();
            }
        }
        if (other.gameObject.CompareTag("player"))
        {
            Player.gameObject.GetComponent<PlayerController>().score--;
            playerScript.setCountText();
            collisionText.text = "Player 2 Hits!!!";
            badCollide.Play();
            collisionText.gameObject.SetActive(true);
            StartCoroutine(wait3Seconds());
        }
    }
    public void setCountText()
    {
        if(GameOver != true)
        countText.text = "Score:  " + score.ToString();
    }
    void addThrust(Vector3 movement)
    {
        rb.AddForce(movement * thrust);
    }
    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        collisionText.gameObject.SetActive(false);
    }
   
    

}
