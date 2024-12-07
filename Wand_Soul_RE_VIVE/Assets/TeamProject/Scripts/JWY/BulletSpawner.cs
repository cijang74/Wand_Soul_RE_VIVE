using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // ¹ß»çÇÒ ÃÑÅº ÇÁ¸®ÆÕ
    [SerializeField] private float fireRate = 0.5f;   // ¹ß»ç °£°Ý
    [SerializeField] private int bulletCount = 8;    // ÃÑÅº ¹ß»ç °³¼ö
    [SerializeField] private float bulletSpeed = 5f; // ÃÑÅº ¼Óµµ
    [SerializeField] private float lifetime = 5f;    // ½ºÆ÷³Ê¿Í ÃÑÅºÀÇ Áö¼Ó ½Ã°£

    private Vector3 direction; // ÃÑÅº ¹ß»ç ¹æÇâ

    public void Initialize(Vector3 spawnDirection)
    {
        direction = spawnDirection.normalized; // ¹æÇâ ÃÊ±âÈ­
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        float angleStep = 360f / bulletCount; // °¢ ÃÑÅºÀÇ °¢µµ °£°Ý
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // °¢µµ¿¡ µû¸¥ ¹ß»ç ¹æÇâ °è»ê
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(bulletDirX, bulletDirY, 0).normalized;

            // ÃÑÅº »ý¼º
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, lifetime); // ÃÑÅº ÀÏÁ¤ ½Ã°£ ÈÄ ÆÄ±«

            angle += angleStep; // °¢µµ Áõ°¡
        }

        yield return new WaitForSeconds(fireRate);

        // ½ºÆ÷³Ê ÆÄ±«
        Destroy(gameObject);
    }
}
