using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
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
            if (collision.gameObject.tag=="enemy")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "plane")
            {
                Debug.Log("GameOver");
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
