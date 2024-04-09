using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public CircleCollider2D spawnArea; // 스폰 위치 결정에 사용될 CircleCollider2D의 참조
    public float startTime = 1; // 몬스터 생성 시작 시간
    public float spawnDuration = 30; // 몬스터 생성 지속 시간

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

            // CircleCollider2D의 위치 및 반지름을 기반으로 무작위 위치 결정
            Vector2 center = spawnArea.transform.position;
            float radius = spawnArea.radius;
            Vector2 spawnPosition = center + Random.insideUnitCircle * radius;

            // 해당 위치에 몬스터 생성
            Instantiate(monsters[monsterIndex], spawnPosition, Quaternion.identity);
        }

        if (monsterIndex + 1 < monsters.Length)
        {
            StartCoroutine(SpawnMonsters(monsterIndex + 1, duration + 5)); // 다음 몬스터 생성 시작
        }
    }
}
