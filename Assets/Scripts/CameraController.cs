/*
Author: Kyle Peniston
Date: 9/11/2024
Description: CameraController.cs manages the camera's position relative to the player 
while maintaining a consistent offset. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //Camera ZAxis offset
        offset = transform.position - player.transform.position; // offset vector from initial config
    }

    // Update is called once per frame
    void Update()
    {
        //Allow Camera to follow player position
        transform.position = player.transform.position + offset;
    }
}
