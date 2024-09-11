/*
Author: Kyle Peniston
Date: 9/11/2024
Description: Rotator.cs controls the rotation, speed and random movement of pickup game objects. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] public float pickupSpeed = 1f;
    [SerializeField] float rotationSpeed = 45f;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
    void FixedUpdate()
    {
        //Random movement
        float xAxis = Random.Range(-1.0f, 1.0f);
        float yAxis = Random.Range(-1.0f, 1.0f);

        Vector2 movement = new Vector2(xAxis, yAxis).normalized;
        rb2d.velocity = movement * pickupSpeed;
    }

    public void SetSpeed(float _newSpeed)
    {
        //Used for Pickup object speed
        pickupSpeed = _newSpeed;
    }
}
