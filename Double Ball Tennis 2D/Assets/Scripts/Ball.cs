using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    float startingForce = 5f;
    GameManager gameManager;
    float speedMultiplier = 1.2f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        if (transform.position.x > 0)
        {
            startingForce *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity *= new Vector2(speedMultiplier, 1);
        gameManager.audioSource.PlayOneShot(gameManager.sfx[0]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.audioSource.PlayOneShot(gameManager.sfx[1]);

        if (collision.gameObject.name == "Left")
        {
            GameManager.p2Score++;
            gameManager.CheckForGameWinner();
            gameManager.ShowPointWinner(gameManager.p2Name);
        }
        else if (collision.gameObject.name == "Right")
        {
            GameManager.p1Score++;
            gameManager.CheckForGameWinner();
            gameManager.ShowPointWinner(gameManager.p1Name);
        }

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) 
        {
            Destroy(ball);
        }

        if(!gameManager.gameOver)
        {
            gameManager.StartCoroutine("ResetRound");
        }
    }
}
