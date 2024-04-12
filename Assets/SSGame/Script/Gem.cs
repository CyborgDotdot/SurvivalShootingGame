using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int expValue = 1; // ȹ���� �� �ִ� ����ġ�� ���� ������ �� �ְ� �մϴ�.

    public float attractRange = 3f; // �÷��̾ ���� �����̱� �����ϴ� ����
    public float attractPower = 5f; // ������ �����̴� �ӵ�

    private GameObject player; // �÷��̾� ������Ʈ�� ���� ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // ������ �� �÷��̾� ������Ʈ�� ã���ϴ�.
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

        // �Ÿ��� ���� �ӵ� ����
        float speedModifier = Mathf.Clamp01(1 - distance / attractRange);
        Vector3 newVelocity = direction.normalized * attractPower * speedModifier;

        // ������ �÷��̾� ������ �̵���ŵ�ϴ�.
        transform.position += newVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
