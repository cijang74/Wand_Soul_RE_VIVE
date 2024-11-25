using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEnemy : Enemy
{
    Rigidbody2D rd2d;
    CapsuleCollider2D EnemyCapsule;


    [SerializeField] float findDistance = 10f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float attackCooldown = 2f; // 공격 간격

    private Transform target;
    private Animator animator;

    private bool isAttacking = false;

    TreeEnemy()
    {
        hp = 10;
    }

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        EnemyCapsule = GetComponent<CapsuleCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(hp <= 0)
        {
            Die();
            return;
        }
        if(target == null)
        { return; }

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        

        if(distanceToPlayer > findDistance)
        {
            StopMoving();
        }
        else if(distanceToPlayer <= attackDistance)
        {
            if(!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            MoveTowardPlayer();
        }
    }

    private void StopMoving()
    {
        rd2d.velocity = new Vector2(0, rd2d.velocity.y);
    }

    private void MoveTowardPlayer()
    {
        if(!isAttacking)
        {
            float moveDirection = target.position.x > transform.position.x ? 1 : -1;
            Vector2 enemyVelocity = new Vector2(moveSpeed * moveDirection, rd2d.velocity.y);
            rd2d.velocity = enemyVelocity;
            if(moveDirection < 0)
                FlipEnemyFacing();
            else if(moveDirection > 0)
                FlipEnemyFacing();
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        StopMoving();

        animator.SetTrigger("isAttacking");

        yield return new WaitForSeconds(attackCooldown);

        animator.ResetTrigger("isAttacking");
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Damage(5);
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rd2d.velocity.x)), 1f);
    }
}
