using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI waveTimerText;

    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    private int previousMaxExperience = 0;


    [SerializeField] private TextMeshProUGUI roundTimer;

    [SerializeField] private GameObject LevelUpButton;
    private float currentTime;

    private bool needToUpdateSlider;
    public float timeSpeed = 1;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Increment the current time
        currentTime += Time.deltaTime;

        // Convert the time to minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the text to display the formatted time
        roundTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetLivesText(int amount)
    {
        livesText.text = amount.ToString();
    }

    public void SetWaveTimer(int time)
    {
        waveTimerText.gameObject.SetActive(true);
        waveTimerText.text = time.ToString();
    
        if(time <= 0)
        {
            waveTimerText.gameObject.SetActive(false);
        }
    }
    
    public void ShowHideLevelUpButton(bool show)
    {
        LevelUpButton.SetActive(show);
    }
    
    public void SetLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetExperienceText(int experience, int maxExperience, bool leveledUp)
    {
        experienceSlider.value = experience;
        if (leveledUp)
        {
            previousMaxExperience += maxExperience;
        }
        experienceSlider.maxValue = maxExperience;
        experienceText.text = (experience + previousMaxExperience) + "/" + (maxExperience + previousMaxExperience);
    }

    public void OnLevelUpExpirienceText(int experience, int maxExperience)
    {
        experienceSlider.maxValue = experienceSlider.maxValue + maxExperience;
        experienceText.text = (experienceSlider.value + experience) + "/" + (experienceSlider.maxValue + maxExperience);
    }
}
