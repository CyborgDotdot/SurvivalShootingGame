using UnityEngine;
using System.Linq;
using System.Collections;

public class Weapon_Knife : Weapon
{
    public GameObject upAttackObject;
    public GameObject downAttackObject;
    public GameObject leftAttackObject;
    public GameObject rightAttackObject;

    private GameObject currentActiveAttackObject;

    private Player player; // Player 컴포넌트를 참조하기 위한 변수

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void StartAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        ActivateAttackObjectBasedOnDirection();
        yield return new WaitForSeconds(attackDuration);

        if (currentActiveAttackObject != null)
        {
            // 자식 오브젝트의 BoxCollider2D 컴포넌트를 사용하여 충돌 검사를 수행
            BoxCollider2D collider = currentActiveAttackObject.GetComponent<BoxCollider2D>();

            // OverlapBoxAll 함수를 호출할 때, 자식 오브젝트의 BoxCollider2D 컴포넌트를 사용
            var hitColliders = Physics2D.OverlapBoxAll(collider.transform.position, collider.size, 0, monsterLayerMask);
            var nearestMonsters = hitColliders.OrderBy(h => (h.transform.position - transform.position).sqrMagnitude).Take(3);

            foreach (var monster in nearestMonsters.Select(hitCollider => hitCollider.GetComponent<Monster>()).Where(monster => monster != null))
            {
                monster.TakeDamage(damage);
            }

            currentActiveAttackObject.SetActive(false);
        }

        yield return new WaitForSeconds(cooldown - attackDuration);
        isAttacking = false;
    }

    private void ActivateAttackObjectBasedOnDirection()
    {
        DisableAllAttackObjects();

        if (player != null)
        {
            switch (player.lastInputDirection)
            {
                case "back":
                    currentActiveAttackObject = upAttackObject;
                    break;
                case "front":
                    currentActiveAttackObject = downAttackObject;
                    break;
                case "left":
                    currentActiveAttackObject = leftAttackObject;
                    break;
                case "right":
                    currentActiveAttackObject = rightAttackObject;
                    break;
            }

            if (currentActiveAttackObject != null)
            {
                currentActiveAttackObject.SetActive(true);
            }
        }
    }

    private void DisableAllAttackObjects()
    {
        upAttackObject.SetActive(false);
        downAttackObject.SetActive(false);
        leftAttackObject.SetActive(false);
        rightAttackObject.SetActive(false);
    }
}