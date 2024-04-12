using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health Settings")]
    public Slider healthBar;
    public float maxHealth = 10f;
    private float currentHealth;
    public float healthRecoveryRate = 0.1f; // ü���� ȸ���Ǵ� ���� (��: 0.1 = �ִ� ü���� 10%)
    public float healthRecoveryInterval = 5f; // ü���� ȸ���Ǵ� �ð� ���� (��)
    private float healthRecoveryTimer = 0f; // Ÿ�̸� ����

    [Header("Experience Settings")]
    public Slider expBar;
    private int currentExp = 0;
    public int expToLevelUp = 100; // �������� ���� �ʿ� ����ġ

    [Header("Level Up UI")]
    public GameObject levelUpUI;

    [Header("Effects")]
    public GameObject playerEffect;

    [Header("Game Over UI")]
    public GameObject gameOverUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        expBar.maxValue = expToLevelUp;
        expBar.value = 0;

        levelUpUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        // ü�� ȸ�� ����
        if (currentHealth < maxHealth)
        {
            healthRecoveryTimer += Time.deltaTime;
            if (healthRecoveryTimer >= healthRecoveryInterval)
            {
                RecoverHealth();
                healthRecoveryTimer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }
    }
    void RecoverHealth()
    {
        float healthToRecover = maxHealth * healthRecoveryRate;
        currentHealth += healthToRecover;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

    public void GainExp(int exp)
    {
        currentExp += exp;
        expBar.value = currentExp;
        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Time.timeScale = 0f;
        levelUpUI.SetActive(true);

        currentExp = 0;
        expBar.value = 0;
        // �߰����� ������ ó�� ����
    }

    public void ResumeGame()
    {
        // ���� �簳
        Time.timeScale = 1f;
        levelUpUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameObject playerObject = GameObject.Find("Player");
        DeathEffect(playerObject);
        playerObject.SetActive(false);
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    private void DeathEffect(GameObject playerObject)
    {
        GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
}