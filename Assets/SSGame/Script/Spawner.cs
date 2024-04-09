using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap ����� ���� �ʿ�

public class Spawner : MonoBehaviour
{
    public Tilemap tilemap; // ���� ��ġ ������ ���� Tilemap�� ����
    public float startTime = 1; // ���� ���� ���� �ð�
    public float spawnDuration = 10; // ���� ���� ���� �ð�

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

            // TilemapCollider2D�� ��踦 ������� ������ ��ġ ����
            Vector3 min = tilemap.localBounds.min;
            Vector3 max = tilemap.localBounds.max;
            float x = Random.Range(min.x, max.x);
            float y = Random.Range(min.y, max.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // ������ ��ġ�� ���� Ÿ�� ���� �ִ��� Ȯ��
            Vector3Int cellPosition = tilemap.WorldToCell(spawnPosition);
            if (tilemap.HasTile(cellPosition))
            {
                Instantiate(monsters[monsterIndex], spawnPosition, Quaternion.identity);
            }
        }

        if (monsterIndex + 1 < monsters.Length)
        {
            StartCoroutine(SpawnMonsters(monsterIndex + 1, duration + 20)); // ���� ���� ���� ����
        }
    }
}
