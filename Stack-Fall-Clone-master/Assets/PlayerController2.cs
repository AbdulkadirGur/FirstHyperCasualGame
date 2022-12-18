using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;
    
    float currentTime;
    bool invincible;

    public GameObject fireShield;

    [SerializeField]
    AudioClip win, death, idestroy, destroy, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;

    public Image InvictableSlider;
    public GameObject InvictableOBJ;

    public GameObject finisUI;
    public GameObject gameOverUI;

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
      

    }
    void Start()
    {
        totalObstacleNumber = FindObjectsOfType<ObstacleController2>().Length;
        
    }


    void Update()
    {

        if(playerstate == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                carpma = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                carpma = false;
            }

            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;
                if (!fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(true);
                }
            }
            else
            {
                if (fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(false);
                }
                if (carpma)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }



            if (currentTime >= 0.15f || InvictableSlider.color == Color.red)
            {
                InvictableOBJ.SetActive(true);
            }
            else
            {
                InvictableOBJ.SetActive(false);
            }


            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                Debug.Log("Invicible");
                InvictableSlider.color=Color.red;   
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                Debug.Log("====================");
                InvictableSlider.color = Color.white;
            }


            if (InvictableOBJ.activeInHierarchy)
            {
                InvictableSlider.fillAmount = currentTime / 1;


            }

        }

        /*if(playerstate == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
            {
                playerstate = PlayerState.Playing;
            }
        }*/

        if (playerstate == PlayerState.Finish)
        {   
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner2>().NextLevel();
            }
           
        }
        
        
    }


    public void shatterObsctacle()
    {
        if (invincible)
        {
            ScoreManager2.instance.addScore(2);

        }
        else
        {
            ScoreManager2.instance.addScore(1 );        
        }
    }
    private void FixedUpdate()
    {
        if(playerstate == PlayerState.Playing)
        {
            if (carpma)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!carpma)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" ||  collision.gameObject.tag == "plane")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController2>().ShatterAllObstacles();
                    shatterObsctacle();
                    SoundManager2.instance.playSoundFX(idestroy, 0.5f);
                    currentObstacleNumber++;
                }
            }
            else
            {
                if (collision.gameObject.tag=="enemy")
                {
                    // Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController2>().ShatterAllObstacles();
                    shatterObsctacle();
                    SoundManager2.instance.playSoundFX(destroy, 0.5f);
                    currentObstacleNumber++;

                }
                 else if (collision.gameObject.tag == "plane")
                 {
                    Debug.Log("GameOver");
                    gameOverUI.SetActive(true);
                    playerstate = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    ScoreManager2.instance.resetScore();
                    SoundManager2.instance.playSoundFX(death, 0.5f);
                 }
            }

            
        }


        FindObjectOfType<GameUI2>().levelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);



        if(collision.gameObject.tag == "Finish" && playerstate == PlayerState.Playing)
        {
            playerstate = PlayerState.Finish;
            SoundManager2.instance.playSoundFX(win, 0.5f);
            finisUI.SetActive(true);
            finisUI.transform.GetChild(0).GetComponent<Text>().text = "Level" + PlayerPrefs.GetInt("Level",0);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!carpma || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager2.instance.playSoundFX(bounce, 0.5f);
        }
    }
}
