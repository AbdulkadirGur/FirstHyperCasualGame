using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController2 : MonoBehaviour
{
    [SerializeField]
    private Obstacle2[] obstacles =null;


    public void ShatterAllObstacles()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (Obstacle2 item  in obstacles)
        {
            item.Shatter();
        }

        StartCoroutine(RemoveAllShatterParts());


    
}

    IEnumerator RemoveAllShatterParts()
    {
        yield return new  WaitForSeconds(1);
        Destroy(gameObject);    
    }
}
