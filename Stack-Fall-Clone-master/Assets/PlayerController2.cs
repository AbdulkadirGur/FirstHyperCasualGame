using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;
    
    float currentTime;
    bool invincible;

    public GameObject fireShield;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }


    void Update()
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
            currentTime = Time.deltaTime * .35f;
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


        
        if (currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
            Debug.Log("Invicible");
        }
        else if(currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
            Debug.Log("====================");
        }
        
    }

    private void FixedUpdate()
    {
        if (carpma)
        {
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
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
                    Destroy(collision.transform.parent.gameObject);
                }
            }
            else
            {
                if (collision.gameObject.tag=="enemy")
                {
                     Destroy(collision.transform.parent.gameObject);
                }
                 else if (collision.gameObject.tag == "plane")
                 {
                    Debug.Log("GameOver");
                 }
            }

            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!carpma)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
