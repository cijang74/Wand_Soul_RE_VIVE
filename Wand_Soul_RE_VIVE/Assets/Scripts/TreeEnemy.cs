using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEnemy : Enemy
{
    Rigidbody2D rd2d;
    CapsuleCollider2D EnemyCapsule;


    [SerializeField] float findDistance = 10f;
    private Transform target;


    TreeEnemy()
    {
        hp = 10;
    }

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        EnemyCapsule = GetComponent<CapsuleCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
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

        if(distanceToPlayer < findDistance)
        {

        }
    }
}
