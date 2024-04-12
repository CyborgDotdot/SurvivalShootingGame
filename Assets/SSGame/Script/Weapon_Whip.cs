using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Whip : MonoBehaviour
{
    public int damage = 10;
    public float attackDuration = 0.5f; // ���� ���� �ð�
    public float cooldown = 1.5f; // ��Ÿ��

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Weapon: SpriteRenderer ������Ʈ�� �����ϴ�.");
        }

        // ���� ���� �� �ڵ����� ���� �ֱ⸦ �����մϴ�.
        StartCoroutine(AutoAttackRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            if (monster != null && spriteRenderer.enabled == true) // ��������Ʈ�� Ȱ��ȭ�� ���¿����� �������� �ݴϴ�.
            {
                monster.TakeDamage(damage);
            }
        }
    }

    IEnumerator AutoAttackRoutine()
    {
        while (true) // ���� ������ ����Ͽ� �ֱ������� �ݺ��մϴ�.
        {
            StartAttack();
            yield return new WaitForSeconds(attackDuration); // ���� ���� �ð� ���� ���
            EndAttack();
            yield return new WaitForSeconds(cooldown); // ��Ÿ�� ���� ���
        }
    }

    void StartAttack()
    {
        spriteRenderer.enabled = true; // ��������Ʈ�� Ȱ��ȭ�Ͽ� ���� ǥ��
    }

    void EndAttack()
    {
        spriteRenderer.enabled = false; // ��������Ʈ�� ��Ȱ��ȭ�Ͽ� ���� ���� ǥ��
    }
}
