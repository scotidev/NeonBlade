using TMPro;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float maxTime = 60f;
    private float currentTime;
    private bool isGameOver = false;
    public TMP_Text timerText;
    public GameManager gameManager;

    void Start()
    {
        currentTime = 20;
        UpdateTimerUI();
    }


    void Update()
    {
        if (!isGameOver)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isGameOver = true;
                gameManager.GameOver();
            }

            UpdateTimerUI();
        }
    }

    public void AddTime(float timeToAdd)
    {
        if (!isGameOver)
        {
            currentTime += timeToAdd;

            if (currentTime > maxTime)
            {
                currentTime = maxTime;
            }

            UpdateTimerUI();
        }
    }

    public void SubtractTime(float timeToSubtract)
    {
        if (!isGameOver)
        {
            currentTime -= timeToSubtract;

            if (currentTime < 0)
            {
                currentTime = 0;
            }

            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);

        timerText.text = $"{seconds}.<size=70%>{milliseconds:D2}</size>";
    }
}