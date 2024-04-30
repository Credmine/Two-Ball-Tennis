using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    float positionBoundary = 5f;
    KeyCode moveUpKey, moveDownKey;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Initialize control buttons for both players
        if (gameObject.name == "Player 1")
        {
            moveUpKey = KeyCode.W;
            moveDownKey = KeyCode.S;
        }
        else
        {
            moveUpKey = KeyCode.UpArrow;
            moveDownKey = KeyCode.DownArrow;
        }
    }
    void Update()
    {
        if (gameManager.GameIsActive)
        {
            MovePlayer();
        }
        BoundPlayer();
    }
   
    void MovePlayer()
    {
        if (Input.GetKey(moveUpKey))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(moveDownKey))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
    void BoundPlayer()
    {
        if (transform.position.y > positionBoundary)
        {
            transform.position = new Vector2(transform.position.x, positionBoundary);
        }
        if (transform.position.y < -positionBoundary)
        {
            transform.position = new Vector2(transform.position.x, -positionBoundary);
        }
    }
}
