using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 사용을 위해 추가

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Experience Settings")]
    public Slider expBar; // 경험치 바 UI
    private int currentExp = 0;
    public int expToLevelUp = 100; // 레벨업을 위한 필요 경험치

    [Header("Level Up UI")]
    public GameObject levelUpUI; // 레벨업 UI

    [Header("Effects")]
    public GameObject playerEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        expBar.value = CalculateExpProgress();
        levelUpUI.SetActive(false);
    }

    void Update()
    {

    }

    public void GainExp(int exp)
    {
        currentExp += exp;
        expBar.value = CalculateExpProgress();

        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    float CalculateExpProgress()
    {
        return (float)currentExp / expToLevelUp;
    }

    public void LevelUp()
    {
        // 게임 일시정지
        Time.timeScale = 0f;

        // UI Active
        levelUpUI.SetActive(true);

        // 경험치 및 레벨업 관련 처리
        currentExp = 0; // 혹은 currentExp -= expToLevelUp; 로 레벨업 후 남은 경험치를 유지
        expBar.value = CalculateExpProgress();
        // 추가적인 레벨업 처리 로직
    }

    public void ResumeGame()
    {
        // 게임 재개
        Time.timeScale = 1f;
        levelUpUI.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. Player 오브젝트의 이름이 정확한지 확인해주세요.");
        }
        Debug.Log("Game Over!");
    }
}
