using UnityEngine;

public class Weapon_Whip : Weapon
{
    public float pushBackForce; // �о�� ���� ũ��
    public float attackRange; // ���� ����
    private Animator animator; // Animator ������Ʈ�� ���� ����

    protected override void Start()
    {
        base.Start(); // �θ� Ŭ������ Start �޼��� ȣ��
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ
    }

    protected override void StartAttack()
    {
        isAttacking = true; // ���� ���·� ����
        if (animator != null)
        {
            animator.SetBool("isAttacking", true); // �ִϸ��̼� ���� ������Ʈ
        }

        // ���� ���� ���� ��� ���͸� �����ϰ� �о���ϴ�.
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, attackRange, monsterLayerMask);
        foreach (Collider2D monsterCollider in monsters)
        {
            Monster monster = monsterCollider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage); // ���Ϳ��� ���ظ� �ݴϴ�.
                PushBack(monster); // �о���ϴ�.
            }
        }
    }

    protected override void EndAttack()
    {
        base.EndAttack(); // �θ� Ŭ������ EndAttack �޼��� ȣ��
        if (animator != null)
        {
            animator.SetBool("isAttacking", false); // �ִϸ��̼� ���� ������Ʈ
        }
    }

    // ���͸� �о�� �޼���
    private void PushBack(Monster monster)
    {
        Rigidbody2D monsterRigidbody = monster.GetComponent<Rigidbody2D>();
        if (monsterRigidbody != null)
        {
            Vector2 forceDirection = monsterRigidbody.position - (Vector2)transform.position; // �о�� ����
            forceDirection.Normalize(); // ���� ���͸� ���� ���ͷ� ����
            monsterRigidbody.AddForce(forceDirection * pushBackForce, ForceMode2D.Impulse); // �о�� �� ����
            monster.SetPushedBack(true);
        }
    }
}
