using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSource : MonoBehaviour
{
    [SerializeField] private int DamageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) // other과 충돌하면 실행되는 메서드
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        player?.TakeDamage(DamageAmount, transform);
    }
}