using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon_Knife : Weapon
{
    public BoxCollider2D[] directionColliders; // ��, ��, ��, �� ������ �ݶ��̴���
    private Animator playerAnimator; // Player�� Animator

    protected override void Start()
    {
        base.Start();
        playerAnimator = GetComponentInParent<Animator>();
    }

    protected override void StartAttack()
    {
        isAttacking = true;
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }

        // �÷��̾ �ٶ󺸴� ���⿡ ���� ������ ������ �ݶ��̴��� Ȱ��ȭ
        BoxCollider2D activeCollider = ActivateColliderBasedOnDirection();
        if (activeCollider != null)
        {
            // �ݶ��̴��� Ʈ���� ���� �ȿ� �ִ� ��� ���͸� ã�� ���� ����� 3������ ����
            List<Collider2D> hitMonsters = new List<Collider2D>(Physics2D.OverlapBoxAll(activeCollider.bounds.center, activeCollider.size, 0, monsterLayerMask));
            hitMonsters = hitMonsters.OrderBy(h => (h.transform.position - this.transform.position).sqrMagnitude).Take(3).ToList();

            foreach (Collider2D hitMonster in hitMonsters)
            {
                Monster monster = hitMonster.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.TakeDamage(damage);
                }
            }
        }
    }

    private BoxCollider2D ActivateColliderBasedOnDirection()
    {
        // ��� �ݶ��̴��� ��Ȱ��ȭ
        foreach (var collider in directionColliders)
        {
            collider.enabled = false;
        }

        // �÷��̾ �ٶ󺸴� ���⿡ ���� ������ �ݶ��̴��� Ȱ��ȭ
        BoxCollider2D activeCollider = null;
        if (playerAnimator.GetBool("front"))
            activeCollider = directionColliders.FirstOrDefault(c => c.name == "Front");
        else if (playerAnimator.GetBool("back"))
            activeCollider = directionColliders.FirstOrDefault(c => c.name == "Back");
        else if (playerAnimator.GetBool("left"))
            activeCollider = directionColliders.FirstOrDefault(c => c.name == "Left");
        else if (playerAnimator.GetBool("right"))
            activeCollider = directionColliders.FirstOrDefault(c => c.name == "Right");

        if (activeCollider != null)
        {
            activeCollider.enabled = true;
        }

        return activeCollider;
    }
}
