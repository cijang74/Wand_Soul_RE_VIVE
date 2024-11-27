using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEnemy : Enemy
{
    Rigidbody2D rd2d;
    CapsuleCollider2D EnemyCapsule;


    [SerializeField] float findDistance = 10f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float attackCooldown = 2f; // 공격 애니메이션 재생 시간
    [SerializeField] float idleTimeAfterAttack = 3f; // 공격 속도

    private Transform target;
    private Animator animator;

    private bool isAttacking = false;

    private bool canAttack = true; // 공격 가능 상태 플래그

    ThunderEnemy()
    {
        hp = 8;
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
        rd2d.velocity = new Vector2(0, rd2d.velocity.y);
    }

    private void MoveTowardPlayer()
    {
        if (!isAttacking)
        {
            float moveDirection = target.position.x > transform.position.x ? 1 : -1;
            Vector2 enemyVelocity = new Vector2(moveSpeed * moveDirection, rd2d.velocity.y);
            rd2d.velocity = enemyVelocity;
            if (moveDirection < 0)
                FlipEnemyFacing();
            else if (moveDirection > 0)
                FlipEnemyFacing();
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        canAttack = false; // 일정 시간 동안 공격 불가능

        StopMoving(); // 공격 중에는 멈춤
        animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(attackCooldown);

        animator.ResetTrigger("isAttack");

        isAttacking = false; // 공격 애니메이션이 끝난 직후 isAttacking을 false로 설정

        // idle 또는 이동 상태로 전환
        yield return new WaitForSeconds(idleTimeAfterAttack);

        canAttack = true; // 다시 공격 가능
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
        else if (!isAttacking) // 공격 중이 아닐 때만 이동
        {
            MoveTowardPlayer();
        }
    }
}
