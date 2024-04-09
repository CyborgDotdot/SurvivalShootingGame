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

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            transform.Translate(dir * mSpeed * Time.deltaTime);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
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
            Destroy(gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            ItemDrop();
        }
    }

    public void TakeDamage(int damage)
    {
        mHealth -= damage;

        if (mHealth <= 0)
        {
            GameObject _mEffect = Instantiate(mEffect, transform.position, Quaternion.identity);
            Destroy(_mEffect,1);
            ItemDrop();
            Destroy(gameObject);
        }
    }

    private void ItemDrop()
    {
        Instantiate(mItem, transform.position, Quaternion.identity);
    }
}
