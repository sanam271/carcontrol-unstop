/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Timertime = 10f;

    // Update is called once per frame
    void Update()
    {
        if(Timertime > 0.01f)
        {
            Debug.Log(Timertime);
            Timertime = Timertime - Time.deltaTime;
        }
    }
}
*/
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameScript : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5f;
    private float originalSpeed;

    // Boost
    public float boostTime = 5f;
    private float boostTimer = 0f;
    private bool isBoosting = false;

    // Timer for gameplay
    public float gameTime = 60f; // 1 minute countdown
    public Text timerText;

    // Coins & Boosters
    private int coinCount = 0;
    private int boosterCount = 0;
    public Text coinText;
    public Text boosterText;
    public GameObject winPanel;
    private bool gameFinished = false;
    void Start()
    {
        originalSpeed = moveSpeed;
        UpdateUI();
    }

    void Update()
    {
        // Player movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, 0, moveZ));

        // Game timer countdown
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.CeilToInt(gameTime).ToString();
        }
        else
        {
            timerText.text = "Time: 0";
            // You could stop the game here if needed
        }

        // Boost countdown
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                EndBoost();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameFinished)
        { 
            return; 
        }
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Booster"))
        {
            boosterCount++;
            UpdateUI();
            Destroy(other.gameObject);
            StartBoost();
        }
        else if(other.CompareTag("Finish"))
        {
            gameFinished = true;
            FinishGame();
        }
    }

    void FinishGame()
    {
        moveSpeed = 0;
        if(winPanel != null) 
            winPanel.SetActive(true);
        Debug.Log("Game Finished! You reached the destination");
    }

    void UpdateUI()
    {
        if (coinText != null) coinText.text = "Coins: " + coinCount;
        if (boosterText != null) boosterText.text = "Boosters: " + boosterCount;
    }

    void StartBoost()
    {
        moveSpeed = originalSpeed * 2;
        isBoosting = true;
        boostTimer = boostTime;
    }

    void EndBoost()
    {
        moveSpeed = originalSpeed;
        isBoosting = false;
    }
}

