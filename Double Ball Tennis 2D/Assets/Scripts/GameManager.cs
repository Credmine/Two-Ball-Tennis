using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI serveText;
    [SerializeField] TextMeshProUGUI pointWonText;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] GameObject[] balls;
    public AudioClip[] sfx;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public static int p1Score, p2Score;
    bool gameIsActive = false;
    public bool GameIsActive { get { return gameIsActive; } }
    float ballDefaultForceMin = 4f, ballDefaultForceMax = 6f;
    float restartRoundDelay = 2;
    [HideInInspector] public string p1Name = "BLUE", p2Name = "ORANGE";
    [HideInInspector] public bool gameOver = false;
    int winningPoint = 3;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        scoreText.text = $"{p1Score} | {p2Score}";

        // Start the game by pressing spacebar
        if (Input.GetKeyDown(KeyCode.Space) && !gameIsActive)
        {
            serveText.gameObject.SetActive(false);
            ServeBalls();
            gameIsActive = true;
        }

        CheckForGameWinner();

        if (Input.GetKey(KeyCode.Space) && gameOver)
        {
            p1Score = 0;
            p2Score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ServeBalls()
    {
        if (balls != null)
        {
            foreach (GameObject ball in balls)
            {
                Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
                float startingForce = Random.Range(ballDefaultForceMin, ballDefaultForceMax);
                if (ball.transform.position.x > 0)
                {
                    startingForce *= -1;
                }
                ballRB.AddForce(Vector2.right * startingForce, ForceMode2D.Impulse);
            }
        }
        audioSource.PlayOneShot(sfx[0]);
    }

    public void ShowPointWinner(string pointWinner)
    {
        if (!gameOver)
        {
            pointWonText.text = $"{pointWinner} WINS A POINT";
            pointWonText.gameObject.SetActive(true);
        }
    }

    public IEnumerator ResetRound()
    {
        if (!gameOver)
        {
            yield return new WaitForSeconds(restartRoundDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void CheckForGameWinner()
    {
        if (p1Score == winningPoint && p2Score == winningPoint)
        {
            winnerText.text = $"IT'S A DRAW!";
            winnerText.gameObject.SetActive(true);
            gameOver = true;
            return;
        }
        else if (p1Score == winningPoint && p2Score < winningPoint)
        {
            winnerText.text = $"{p1Name} WINS THE GAME!";
            winnerText.gameObject.SetActive(true);
            gameOver = true;
        }
        else if (p2Score == winningPoint && p1Score < winningPoint)
        {
            winnerText.text = $"{p2Name} WINS THE GAME!";
            winnerText.gameObject.SetActive(true);
            gameOver = true;
        }
        
    }
}
