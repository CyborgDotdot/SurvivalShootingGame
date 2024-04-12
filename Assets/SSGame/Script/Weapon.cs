using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public float attackDuration;
    public float cooldown;
    public LayerMask monsterLayerMask; // 몬스터 레이어 마스크

    protected bool isAttacking = false;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AutoAttackRoutine());
    }

    IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            StartAttack();
            yield return new WaitForSeconds(attackDuration);
            EndAttack();
            yield return new WaitForSeconds(cooldown);
        }
    }

    protected abstract void StartAttack();

    protected virtual void EndAttack()
    {
        isAttacking = false;
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
