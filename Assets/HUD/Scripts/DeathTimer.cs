using System.Collections;
using TMPro;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float maxTime = 60f;
    private float currentTime;
    private bool isGameOver = false;
    public TMP_Text timerText;
    public GameManager gameManager;

    [Header("Clock Sound")]
    public AudioSource clockAudioSource;
    public AudioClip clockClip;

    [Header("UI Feedback")]
    public Color normalColor = Color.white;
    public Color positiveColor = Color.green;
    public Color negativeColor = Color.red;
    public float flashDuration = 0.2f;
    public float flashScale = 1.3f;
    private float originalFontSize;

    void Start()
    {
        currentTime = 30;
        timerText.color = normalColor;
        originalFontSize = timerText.fontSize;
        UpdateTimerUI();

        if (clockAudioSource != null && clockClip != null)
        {
            clockAudioSource.clip = clockClip;
            clockAudioSource.loop = true;
            clockAudioSource.volume = 0.2f;
            clockAudioSource.Play();
        }
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

            StartCoroutine(FlashTimerColor(positiveColor));
        }
    }

    public void SubtractTime(float timeToSubtract)
    {
        if (!isGameOver)
        {
            if (SFXManager.Instance != null && SFXManager.Instance.hurtClip != null)
            {
                SFXManager.Instance.PlaySound(SFXManager.Instance.hurtClip);
            }

            currentTime -= timeToSubtract;

            if (currentTime < 0)
            {
                currentTime = 0;
                isGameOver = true;
                gameManager.GameOver();
            }

            UpdateTimerUI();

            StartCoroutine(FlashTimerColor(negativeColor));
        }
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);

        timerText.text = $"{seconds}.<size=70%>{milliseconds:D2}</size>";
    }

    public void PauseClockSound()
    {
        if (clockAudioSource != null && clockAudioSource.isPlaying)
        {
            clockAudioSource.Pause();
        }
    }

    public void ResumeClockSound()
    {
        if (clockAudioSource != null && !clockAudioSource.isPlaying && currentTime > 0)
        {
            clockAudioSource.UnPause();
        }
    }

    private IEnumerator FlashTimerColor(Color flashColor)
    {
        StopCoroutine(nameof(FlashTimerColor));

        timerText.color = flashColor;
        timerText.fontSize = originalFontSize * flashScale;

        yield return new WaitForSeconds(flashDuration);

        timerText.color = normalColor;
        timerText.fontSize = originalFontSize;
    }
}