using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rigidbody;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") <= -0.5f)
            animator.SetBool("left", true);
        else
            animator.SetBool("left", false);

        if (Input.GetAxis("Horizontal") >= 0.5f)
            animator.SetBool("right", true);
        else
            animator.SetBool("right", false);

        if (Input.GetAxis("Vertical") >= 0.5f)
            animator.SetBool("back", true);
        else
            animator.SetBool("back", false);

        if (Input.GetAxis("Vertical") <= -0.5f)
            animator.SetBool("front", true);
        else
            animator.SetBool("front", false);

        // ������ �� ��ġ ���
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // ����Ʈ ��ǥ�� ��ȯ
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(newPosition);

        // ����Ʈ ��ǥ�� ����Ͽ� Player�� ȭ�� ������ ������ �ʵ��� ����
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f, 1f);

        // ���ѵ� ����Ʈ ��ġ�� �ٽ� ���� ��ǥ�� ��ȯ
        Vector3 clampedPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
        clampedPosition.z = 0; // Z ��ǥ�� ��ȭ��Ű�� ����

        // Player �̵�
        transform.position = clampedPosition;
    }
    public void TakeDamage(int damage)
    {
        GameManager.instance.TakeDamage(damage);
    }
}