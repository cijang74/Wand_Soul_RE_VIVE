using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // 발사할 총탄 프리팹
    [SerializeField] private float fireRate = 0.5f;   // 발사 간격
    [SerializeField] private int bulletCount = 8;    // 총탄 발사 개수
    [SerializeField] private float bulletSpeed = 5f; // 총탄 속도
    [SerializeField] private float lifetime = 5f;    // 스포너와 총탄의 지속 시간

    private Vector3 direction; // 총탄 발사 방향

    public void Initialize(Vector3 spawnDirection)
    {
        direction = spawnDirection.normalized; // 방향 초기화
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        float angleStep = 360f / bulletCount; // 각 총탄의 각도 간격
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // 각도에 따른 발사 방향 계산
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(bulletDirX, bulletDirY, 0).normalized;

            // 총탄 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, lifetime); // 총탄 일정 시간 후 파괴

            angle += angleStep; // 각도 증가
        }

        yield return new WaitForSeconds(fireRate);

        // 스포너 파괴
        Destroy(gameObject);
    }
}
