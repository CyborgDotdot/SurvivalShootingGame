using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap 사용을 위해 필요

public class Spawner : MonoBehaviour
{
    public Tilemap tilemap; // 스폰 위치 결정에 사용될 Tilemap의 참조
    public float startTime = 1; // 몬스터 생성 시작 시간
    public float spawnDuration = 10; // 몬스터 생성 지속 시간

    [SerializeField]
    private GameObject[] monsters;

    void Start()
    {
        if (tilemap != null)
        {
            StartCoroutine(SpawnMonsters(0, spawnDuration));
        }
        else
        {
            Debug.LogError("Tilemap is not assigned in the Spawner.");
        }
    }

    IEnumerator SpawnMonsters(int monsterIndex, float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            yield return new WaitForSeconds(startTime);

            // TilemapCollider2D의 경계를 기반으로 무작위 위치 결정
            Vector3 min = tilemap.localBounds.min;
            Vector3 max = tilemap.localBounds.max;
            float x = Random.Range(min.x, max.x);
            float y = Random.Range(min.y, max.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // 생성된 위치가 실제 타일 위에 있는지 확인
            Vector3Int cellPosition = tilemap.WorldToCell(spawnPosition);
            if (tilemap.HasTile(cellPosition))
            {
                Instantiate(monsters[monsterIndex], spawnPosition, Quaternion.identity);
            }
        }

        if (monsterIndex + 1 < monsters.Length)
        {
            StartCoroutine(SpawnMonsters(monsterIndex + 1, duration + 20)); // 다음 몬스터 생성 시작
        }
    }
}
