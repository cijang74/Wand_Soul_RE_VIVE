using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 3f; // �ı��� �ð� (�⺻��: 3��)
    [SerializeField] private int damage = 1; // �÷��̾�� �� ������


    private void Start()
    {
        // destroyDelay �Ŀ� ������Ʈ �ı�
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �÷��̾��� ���
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // �÷��̾�� ������ ����
                playerHealth.TakeDamage(damage, transform);
            }
        }
    }
}
