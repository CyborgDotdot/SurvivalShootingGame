using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int expValue = 1; // 획득할 수 있는 경험치의 양을 설정할 수 있게 합니다.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player 컴포넌트를 안전하게 얻습니다.
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