using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    GameObject target; //플레이어
    public float speed = 3.0f;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {

    }

    void Update()
    {
        //플레이어를 태그로 찾기
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            //A - B -> A를 바라보는 벡터
            dir = target.transform.position - transform.position;
            //방향 벡터만 구하기:단위벡터 1의 크기로 만든다
            dirNo = dir.normalized;
        }

        transform.Translate(dirNo * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
