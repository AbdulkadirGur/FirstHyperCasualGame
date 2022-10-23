using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTakip2 : MonoBehaviour
{
    private Vector3 cameraPos;
    private Transform player, win;

    private float cameraOffset;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
           
        }
        if (transform.position.y > player.transform.y)
        {

        }
    }
}
