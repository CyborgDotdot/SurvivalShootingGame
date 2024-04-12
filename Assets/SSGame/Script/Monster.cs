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
        if (target != null && !isPushedBack) // �з��� ���°� �ƴ� ���� �̵�
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            rb.velocity = dir * mSpeed;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // �з��� ���¸� �����ϴ� �޼��� �߰�
    public void SetPushedBack(bool state)
    {
        isPushedBack = state;
        if (state)
        {
            // �з��� �� ���� �ð� ���� �̵��� ���߰� ��
            Invoke(nameof(ResetPushedBack), 0.5f); // 0.5�� �Ŀ� �з��� ���¸� ����
        }
    }

    // �з��� ���¸� �����ϴ� �޼���
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