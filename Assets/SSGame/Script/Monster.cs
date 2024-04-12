using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameObject target;
    public float mSpeed = 3.0f;
    public int mHealth;
    public int mDamage;

    public GameObject mEffect;
    public GameObject mItem;

    private Rigidbody2D rb;
    private bool isPushedBack = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null && !isPushedBack) // 밀려남 상태가 아닐 때만 이동
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            rb.velocity = dir * mSpeed;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // 밀려남 상태를 설정하는 메서드 추가
    public void SetPushedBack(bool state)
    {
        isPushedBack = state;
        if (state)
        {
            // 밀려난 후 일정 시간 동안 이동을 멈추게 함
            Invoke(nameof(ResetPushedBack), 0.5f); // 0.5초 후에 밀려남 상태를 리셋
        }
    }

    // 밀려남 상태를 리셋하는 메서드
    private void ResetPushedBack()
    {
        isPushedBack = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(mDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        mHealth -= damage;

        if (mHealth <= 0)
        {
            ItemDrop();
            DeathEffect();
            Destroy(gameObject);
        }
    }

    private void DeathEffect()
    {
        GameObject _mEffect = Instantiate(mEffect, transform.position, Quaternion.identity);
        Destroy(_mEffect, 1);
    }

    private void ItemDrop()
    {
        Instantiate(mItem, transform.position, Quaternion.identity);
    }
}