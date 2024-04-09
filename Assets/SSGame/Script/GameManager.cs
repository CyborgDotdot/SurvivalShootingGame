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
        GameObject playerObject = GameObject.Find("Player"); // Player 오브젝트의 이름이 "Player"라고 가정합니다.
        if (playerObject != null)
        {
            GameObject effect = Instantiate(playerEffect, playerObject.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            // Player 오브젝트를 비활성화합니다.
            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. Player 오브젝트의 이름이 정확한지 확인해주세요.");
        }
        Debug.Log("Game Over!");
    }
}