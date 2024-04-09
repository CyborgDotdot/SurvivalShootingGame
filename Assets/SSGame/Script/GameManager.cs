using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ����� ���� �߰�

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Experience Settings")]
    public Slider expBar; // ����ġ �� UI
    private int currentExp = 0;
    public int expToLevelUp = 100; // �������� ���� �ʿ� ����ġ

    [Header("Level Up UI")]
    public GameObject levelUpUI; // ������ UI

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
        // ���� �Ͻ�����
        Time.timeScale = 0f;

        // UI Active
        levelUpUI.SetActive(true);

        // ����ġ �� ������ ���� ó��
        currentExp = 0; // Ȥ�� currentExp -= expToLevelUp; �� ������ �� ���� ����ġ�� ����
        expBar.value = CalculateExpProgress();
        // �߰����� ������ ó�� ����
    }

    public void ResumeGame()
    {
        // ���� �簳
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
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. Player ������Ʈ�� �̸��� ��Ȯ���� Ȯ�����ּ���.");
        }
        Debug.Log("Game Over!");
    }
}
