﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 3f; // 파괴될 시간 (기본값: 3초)
    [SerializeField] private int damage = 1; // 플레이어에게 줄 데미지


    private void Start()
    {
        // destroyDelay 후에 오브젝트 파괴
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 플레이어일 경우
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // 플레이어에게 데미지 적용
                playerHealth.TakeDamage(damage, transform);
            }
        }
    }
}
