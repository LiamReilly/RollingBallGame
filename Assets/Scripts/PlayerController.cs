using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    public GameObject Player2;
    public Player2Controller player2Script;
    public Text collisionText;
    public AudioSource pickUpSound;
    public AudioSource jumpSound;
    public AudioSource badCollide;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();       
        score = 0;
        setCountText();
        winText.gameObject.SetActive(false);
        GameOver = false;
        collisionText.gameObject.SetActive(false);
            
    }
    void update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameOver == false)
        {
            /*float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            addThrust(movement);
            */
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce((-1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce((1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(0, 0, (1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(0, 0, (-1 * thrust * Time.deltaTime));
            }
            if (Input.GetKeyDown("space") && transform.position.y == 0.5f)
            {
                jumpSound.Play();
                rb.AddForce(0, jumpPower, 0);
            }
         
        }
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("MinigameArea");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Pickup")&&GameOver != true)
        {
            other.gameObject.SetActive(false);
            score ++;
            pickUpSound.Play();
            setCountText();
            GameManager.gameObject.GetComponent<GameManager>().cubeCount--; ;
            /*Player2.gameObject.GetComponent<PlayerController>().NumberOfCubes--;
            if (NumberOfCubes <= 0)
            {
                if (SceneManager.GetActiveScene().name == "2PlayerArea")
                {
                    if (score - Player2.gameObject.GetComponent<Player2Controller>().score == 0)
                    {
                        winText.text = "It's A Tie!!!!";
                    }
                    if (score - Player2.gameObject.GetComponent<Player2Controller>().score > 0)
                    {
                        winText.text = "Player 1 Wins!!!!";
                    }
                    if (score - Player2.gameObject.GetComponent<Player2Controller>().score < 0)
                    {
                        winText.text = "Player 2 Wins!!!!";
                    }
                }
                winText.gameObject.SetActive(true);
                GameOver = true;
                GameManager.gameObject.GetComponent<GameManager>().gameActive = false;
            }*/

        }
       if (other.gameObject.CompareTag("wall")){
            score--;
            badCollide.Play();


            if (GameOver != true)
            {
                setCountText();
            }
       }
        if (other.gameObject.CompareTag("player2"))
        {
            Player2.gameObject.GetComponent<Player2Controller>().score--;
            player2Script.setCountText();
            collisionText.text = "Player 1 Hits!!!";
            badCollide.Play();
            collisionText.gameObject.SetActive(true);
            StartCoroutine(wait3Seconds());
        }
      
            
    }
    public void setCountText()
    {
        if (GameOver != true)
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
