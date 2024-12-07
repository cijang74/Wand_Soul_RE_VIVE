using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // �߻��� ��ź ������
    [SerializeField] private float fireRate = 0.5f;   // �߻� ����
    [SerializeField] private int bulletCount = 8;    // ��ź �߻� ����
    [SerializeField] private float bulletSpeed = 5f; // ��ź �ӵ�
    [SerializeField] private float lifetime = 5f;    // �����ʿ� ��ź�� ���� �ð�

    private Vector3 direction; // ��ź �߻� ����

    public void Initialize(Vector3 spawnDirection)
    {
        direction = spawnDirection.normalized; // ���� �ʱ�ȭ
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        float angleStep = 360f / bulletCount; // �� ��ź�� ���� ����
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // ������ ���� �߻� ���� ���
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(bulletDirX, bulletDirY, 0).normalized;

            // ��ź ����
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, lifetime); // ��ź ���� �ð� �� �ı�

            angle += angleStep; // ���� ����
        }

        yield return new WaitForSeconds(fireRate);

        // ������ �ı�
        Destroy(gameObject);
    }
}
