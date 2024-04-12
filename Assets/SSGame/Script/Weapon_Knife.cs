using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon_Knife : Weapon
{
    public BoxCollider2D[] directionColliders; // 상, 하, 좌, 우 방향의 콜라이더들
    private Animator playerAnimator; // Player의 Animator

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

        // 플레이어가 바라보는 방향에 따라 적절한 방향의 콜라이더를 활성화
        BoxCollider2D activeCollider = ActivateColliderBasedOnDirection();
        if (activeCollider != null)
        {
            // 콜라이더의 트리거 범위 안에 있는 모든 몬스터를 찾아 가장 가까운 3마리를 공격
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
        // 모든 콜라이더를 비활성화
        foreach (var collider in directionColliders)
        {
            collider.enabled = false;
        }

        // 플레이어가 바라보는 방향에 따라 적절한 콜라이더를 활성화
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
