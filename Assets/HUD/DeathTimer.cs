using TMPro;
using UnityEngine;


public class DeathTimer : MonoBehaviour
{
    public float maxTime = 60f;
    private float currentTime;
    public TMP_Text timerText;
    public GameManager gameManager;

    void Start()
    {
        currentTime = maxTime;
        UpdateTimerUI();
    }


    void Update()
    {
        currentTime -= Time.deltaTime;

        // Update the timer UI
        UpdateTimerUI();

        // Check if the timer has reached zero
        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;

        // Ensure the timer doesn't exceed the maximum time
        if (currentTime > maxTime)
        {
            currentTime = maxTime;
        }

        UpdateTimerUI();
    }

    public void SubtractTime(float timeToSubtract)
    {
        currentTime -= timeToSubtract;

        // Ensure the timer doesn't go below zero
        if (currentTime < 0)
        {
            currentTime = 0;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        // Format the time to show seconds and milliseconds
        int seconds = Mathf.FloorToInt(currentTime); // Get the whole seconds
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100); // Get the milliseconds


        timerText.text = $"Time: {seconds}.<size=50%>{milliseconds:D2} </size> "; // Format as "seconds.milliseconds"

    }

    private void GameOver()
    {
        // Call the GameManager to handle Game Over logic
        //gameManager.GameOver();
    }
}