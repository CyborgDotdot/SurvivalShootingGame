using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player player;
    GameObject target;
    public float mSpeed = 3.0f;
    public int mHealth;
    public int mDamage;

    public SpriteRenderer mRenderer;
    public Color originalColor;
    public GameObject mEffect;
    public GameObject mItem;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            dir = target.transform.position - transform.position;
            dirNo = dir.normalized;
        }

        transform.Translate(dirNo * mSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(mDamage);
            Destroy(gameObject);
        }
    }
}