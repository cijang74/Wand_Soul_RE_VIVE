using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy : Enemy
{
    Rigidbody2D rd2d;
    CapsuleCollider2D EnemyCapsule;

    [SerializeField] float findDistance = 10f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float attackCooldown = 2f; // ���� �ִϸ��̼� ��� �ð�
    [SerializeField] float idleTimeAfterAttack = 3f; // ���� �ӵ�

    private Transform target;
    private Animator animator;

    private bool isWalking = false;
    private bool isAttacking = false;
    private bool canAttack = true; // ���� ���� ���� �÷���

    IceEnemy()
    {
        hp = 15;
    }

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        EnemyCapsule = GetComponent<CapsuleCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void StopMoving()
    {
        rd2d.velocity = Vector2.zero;
        //wander애니메이션 중지
        animator.ResetTrigger("isWander");
        isWalking = false;
        //
    }

    private void MoveTowardPlayer()
    {
        //wander애니메이션 추가
        isWalking = true;
        animator.SetTrigger("isWander");
        //
        float moveDirection = target.position.x > transform.position.x ? 1 : -1;
        Vector2 enemyVelocity = new Vector2(moveSpeed * moveDirection, rd2d.velocity.y);
        rd2d.velocity = enemyVelocity;

        if (moveDirection < 0)
            FlipEnemyFacing();
        else if (moveDirection > 0)
            FlipEnemyFacing();
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        canAttack = false; // ���� �ð� ���� ���� �Ұ���

        StopMoving(); // ���� �߿��� ����
        animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(attackCooldown);

        animator.ResetTrigger("isAttack");

        isAttacking = false; // ���� �ִϸ��̼��� ���� ���� isAttacking�� false�� ����

        // idle �Ǵ� �̵� ���·� ��ȯ
        yield return new WaitForSeconds(idleTimeAfterAttack);

        canAttack = true; // �ٽ� ���� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Damage(5);
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rd2d.velocity.x)), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Die();
            return;
        }
        if (target == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer > findDistance)
        {
            StopMoving();
        }
        else if (distanceToPlayer <= attackDistance && canAttack)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else if (!isAttacking) // ���� ���� �ƴ� ���� �̵�
        {
            MoveTowardPlayer();
        }
    }
}
