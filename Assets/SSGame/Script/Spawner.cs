using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public CircleCollider2D spawnArea; // ���� ��ġ ������ ���� CircleCollider2D�� ����
    public float startTime = 1; // ���� ���� ���� �ð�
    public float spawnDuration = 30; // ���� ���� ���� �ð�

    [SerializeField]
    private GameObject[] monsters;

    void Start()
    {
        if (spawnArea != null)
        {
            StartCoroutine(SpawnMonsters(0, spawnDuration));
        }
        else
        {
            Debug.LogError("CircleCollider2D is not assigned in the Spawner.");
        }
    }

    IEnumerator SpawnMonsters(int monsterIndex, float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            yield return new WaitForSeconds(startTime);

            // CircleCollider2D�� ��ġ �� �������� ������� ������ ��ġ ����
            Vector2 center = spawnArea.transform.position;
            float radius = spawnArea.radius;
            Vector2 spawnPosition = center + Random.insideUnitCircle * radius;

            // �ش� ��ġ�� ���� ����
            Instantiate(monsters[monsterIndex], spawnPosition, Quaternion.identity);
        }

        if (monsterIndex + 1 < monsters.Length)
        {
            StartCoroutine(SpawnMonsters(monsterIndex + 1, duration + 5)); // ���� ���� ���� ����
        }
    }
}
