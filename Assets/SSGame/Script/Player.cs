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

        // 잠재적 새 위치 계산
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // 뷰포트 좌표로 변환
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(newPosition);

        // 뷰포트 좌표를 사용하여 Player가 화면 밖으로 나가지 않도록 제한
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f, 1f);

        // 제한된 뷰포트 위치를 다시 월드 좌표로 변환
        Vector3 clampedPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
        clampedPosition.z = 0; // Z 좌표는 변화시키지 않음

        // Player 이동
        transform.position = clampedPosition;
    }
    public void TakeDamage(int damage)
    {
        GameManager.instance.TakeDamage(damage);
    }
}