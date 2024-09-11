/*
Author: Kyle Peniston
Date: 9/11/2024
Description: PlayerController.cs manages the behavior of the player character in the game. 
It handles player movement, game timer, pick-up and power-up interactions, and the win/lose conditions. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    //Player Speed Vars
    public float speed;

    //UI Elements Vars
    private int count;
    public Text countText;
    public Text winText;
    float timer = 60.0f;
    public Button restartButton;

    //PowerUp Vars
    float powerUpTimer = 0.0f;
    public Text powerUpText;
    bool powerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        countText.text = "Timer: " + timer.ToString() + "s";
        powerUpText.text = "";
        winText.text = "";
        restartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //Doesn't alow timer doesn't go negative
        timer = Mathf.Clamp(timer, 0, float.MaxValue);
        int seconds = Mathf.CeilToInt(timer);
        countText.text = "Timer: " + seconds.ToString() + "s";

        //If timer reaches 0
        if (seconds == 0)
        {
            winText.text = "You Win!";
            //Show restart button on win
            restartButton.gameObject.SetActive(true);
        }

        //If powerUp is true, activate powerUp timer
        if (powerUp)
        {
            powerUpTimer -= Time.deltaTime;
            powerUpTimer = Mathf.Clamp(powerUpTimer, 0, float.MaxValue);
            int powerUpSeconds = Mathf.CeilToInt(powerUpTimer);
            powerUpText.text = "PowerUp: " + powerUpSeconds.ToString() + "s";

            //End powerUp
            if (powerUpTimer <= 0)
            {
                powerUp = false;
                powerUpText.text = "";

                //Reset pickup speed when powerup ends
                ChangePickupSpeedReduction(25);
            }
        }
    }

    //FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        //Player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;
    }

    //FixedUpdate is in sync with physics engine
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            //Lose condition
            if (count >= 1) {
                winText.text = "You Lose!";
                restartButton.gameObject.SetActive(true);
            }
        }

        //PowerUp condition
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            powerUp = true;
            powerUpTimer = 5.0f;

            //Allow powerup for 5 seconds
            powerUpText.text = "PowerUp: " + powerUpTimer.ToString() + "s";

            //Speed reduction
            ChangePickupSpeedReduction(5);
        }
    }

    private void ChangePickupSpeedReduction(float _speed)
    {
        //Find all objects tagged as "PickUp"
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            //Set speed
            Rotator pickupSpeed = pickup.GetComponent<Rotator>();
            pickupSpeed.SetSpeed(_speed);
        }
    }

    public void OnRestartButtonPress() {
        //Restart button
        SceneManager.LoadScene("SampleScene");
    }
}
