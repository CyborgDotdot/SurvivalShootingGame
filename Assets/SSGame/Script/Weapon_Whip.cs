using UnityEngine;

public class Weapon_Whip : Weapon
{
    public float pushBackForce; // 밀어내는 힘의 크기
    public float attackRange; // 공격 범위
    private Animator animator; // Animator 컴포넌트에 대한 참조

    protected override void Start()
    {
        base.Start(); // 부모 클래스의 Start 메서드 호출
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
    }

    protected override void StartAttack()
    {
        isAttacking = true; // 공격 상태로 설정
        if (animator != null)
        {
            animator.SetBool("isAttacking", true); // 애니메이션 상태 업데이트
        }

        // 공격 범위 내의 모든 몬스터를 감지하고 밀어냅니다.
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, attackRange, monsterLayerMask);
        foreach (Collider2D monsterCollider in monsters)
        {
            Monster monster = monsterCollider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage); // 몬스터에게 피해를 줍니다.
                PushBack(monster); // 밀어냅니다.
            }
        }
    }

    protected override void EndAttack()
    {
        base.EndAttack(); // 부모 클래스의 EndAttack 메서드 호출
        if (animator != null)
        {
            animator.SetBool("isAttacking", false); // 애니메이션 상태 업데이트
        }
    }

    // 몬스터를 밀어내는 메서드
    private void PushBack(Monster monster)
    {
        Rigidbody2D monsterRigidbody = monster.GetComponent<Rigidbody2D>();
        if (monsterRigidbody != null)
        {
            Vector2 forceDirection = monsterRigidbody.position - (Vector2)transform.position; // 밀어내는 방향
            forceDirection.Normalize(); // 방향 벡터를 단위 벡터로 만듦
            monsterRigidbody.AddForce(forceDirection * pushBackForce, ForceMode2D.Impulse); // 밀어내는 힘 적용
            monster.SetPushedBack(true);
        }
    }
}
