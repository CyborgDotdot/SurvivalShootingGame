using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    GameObject target; //�÷��̾�
    public float speed = 3.0f;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {

    }

    void Update()
    {
        //�÷��̾ �±׷� ã��
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            //A - B -> A�� �ٶ󺸴� ����
            dir = target.transform.position - transform.position;
            //���� ���͸� ���ϱ�:�������� 1�� ũ��� �����
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
