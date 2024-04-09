using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int expValue = 1; // ȹ���� �� �ִ� ����ġ�� ���� ������ �� �ְ� �մϴ�.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player ������Ʈ�� �����ϰ� ����ϴ�.
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.GainExp(expValue); // ������ ����ġ ���� �÷��̾�� �����մϴ�.
                CollectGem(); // ������ �����ϴ� ������ ������ �޼���� �и��մϴ�.
            }
        }
    }

    private void CollectGem()
    {
        // ������ �������� ���� �ǵ�� ������ �߰��� �� �ִ� �����Դϴ�.
        // ��: AudioSource.PlayClipAtPoint(gemCollectSound, transform.position);

        Destroy(gameObject); // ���� ���� ������Ʈ�� �ı��մϴ�.
    }
}