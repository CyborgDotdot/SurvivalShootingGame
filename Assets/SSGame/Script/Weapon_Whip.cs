using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Whip : MonoBehaviour
{
    public int damage = 10;
    public float attackDuration = 0.5f; // 공격 지속 시간
    public float cooldown = 1.5f; // 쿨타임

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Weapon: SpriteRenderer 컴포넌트가 없습니다.");
        }

        // 게임 시작 시 자동으로 공격 주기를 시작합니다.
        StartCoroutine(AutoAttackRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            if (monster != null && spriteRenderer.enabled == true) // 스프라이트가 활성화된 상태에서만 데미지를 줍니다.
            {
                monster.TakeDamage(damage);
            }
        }
    }

    IEnumerator AutoAttackRoutine()
    {
        while (true) // 무한 루프를 사용하여 주기적으로 반복합니다.
        {
            StartAttack();
            yield return new WaitForSeconds(attackDuration); // 공격 지속 시간 동안 대기
            EndAttack();
            yield return new WaitForSeconds(cooldown); // 쿨타임 동안 대기
        }
    }

    void StartAttack()
    {
        spriteRenderer.enabled = true; // 스프라이트를 활성화하여 공격 표시
    }

    void EndAttack()
    {
        spriteRenderer.enabled = false; // 스프라이트를 비활성화하여 공격 종료 표시
    }
}
