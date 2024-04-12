using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int expValue = 1; // 획득할 수 있는 경험치의 양을 설정할 수 있게 합니다.

    public float attractRange = 3f; // 플레이어를 향해 움직이기 시작하는 범위
    public float attractPower = 5f; // 보석이 움직이는 속도

    private GameObject player; // 플레이어 오브젝트에 대한 참조

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 시작할 때 플레이어 오브젝트를 찾습니다.
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attractRange)
            {
                AttractToPlayer();
            }
        }
    }

    private void AttractToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        // 거리에 따라 속도 조절
        float speedModifier = Mathf.Clamp01(1 - distance / attractRange);
        Vector3 newVelocity = direction.normalized * attractPower * speedModifier;

        // 보석을 플레이어 쪽으로 이동시킵니다.
        transform.position += newVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.GainExp(expValue); // 설정된 경험치 값을 플레이어에게 전달합니다.
                CollectGem(); // 보석을 수집하는 로직을 별도의 메서드로 분리합니다.
            }
        }
    }

    private void CollectGem()
    {
        // 보석을 수집했을 때의 피드백 로직을 추가할 수 있는 공간입니다.
        // 예: AudioSource.PlayClipAtPoint(gemCollectSound, transform.position);

        Destroy(gameObject); // 보석 게임 오브젝트를 파괴합니다.
    }
}
