using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UiManager uiManager;
    public XRInteractionManager interactionManager;


    [Header("Experience and Level")]
    [SerializeField] private int level;
    [SerializeField] private int maxExperience;
    private int currentExperience = 0;
    private int skillpoints;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject levelCompleteScreen;

    [SerializeField] private int lives;

    public int enemiesAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        uiManager.SetLivesText(lives);
        InvokeRepeating("ShowLevelUpButton", 0,0.5f);
    }

    public void SetEnemiesAmount(int amount)
    {
        enemiesAmount = amount;
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        UiManager.instance.SetLivesText(lives);

        if(lives <= 0)
        {
            OnGameOver();
        }
    }

    public void OnEnemyDestroyed()
    {
        Invoke("DecreaseEnemies", 0.3f);
    }

    private void DecreaseEnemies()
    {
        enemiesAmount--;
        if (enemiesAmount <= 0)
        {
            WaveSpawner.instance.needToSpawnWave = true;
        }
    }

    public void ShowLevelUpButton()
    {
        uiManager.ShowHideLevelUpButton(skillpoints > 0);
    }

    public void DecreaseSkillpoints()
    {
        skillpoints--;
    }

    public void OnLevelCompletion()
    {
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0.2f;
    }

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.2f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GetExperience(int amount)
    {
        currentExperience += amount;
        uiManager.SetExperienceText(currentExperience, maxExperience, false);
        while(currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // Increase the player's level
        level++;
        skillpoints++;

        // Grant a skill point to the player

        // Increase the maximum experience required for the next level
        maxExperience = CalculateNextLevelExperience();

        // Reset current experience to the remaining experience after leveling up
        currentExperience -= maxExperience;

        // Perform any other actions related to leveling up, such as unlocking new abilities or stats

        // Update UI
        uiManager.SetLevelText(level);
        uiManager.SetExperienceText(currentExperience, maxExperience, true);
    }

    private int CalculateNextLevelExperience()
    {
        // Define the base experience required for level 1
        int baseExperience = 100;

        // Define the experience multiplier to adjust the leveling pace
        float experienceMultiplier = 1.2f;

        // Calculate the experience required for the next level using a formula
        int nextLevelExperience = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level - 1));

        return nextLevelExperience;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}