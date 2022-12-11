using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager2 : MonoBehaviour
{
    public float speed = 1f;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime));
    }
}
