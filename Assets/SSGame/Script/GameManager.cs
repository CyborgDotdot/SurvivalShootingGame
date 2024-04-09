using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 10;
    private int currentHealth;
    public GameObject playerEffect;


    void Start()
    {
        
    }

    void Update()
    {
        
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
        GameObject playerObject = GameObject.Find("Player"); // Player ������Ʈ�� �̸��� "Player"��� �����մϴ�.
        if (playerObject != null)
        {
            GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            // Player ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. Player ������Ʈ�� �̸��� ��Ȯ���� Ȯ�����ּ���.");
        }
        Debug.Log("Game Over!");
    }
}