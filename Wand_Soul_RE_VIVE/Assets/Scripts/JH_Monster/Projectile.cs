using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ȭ�� �߻� ��ũ��Ʈ
    [SerializeField] private float moveSpeed = 22f; // ȭ��ӵ�
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        // �ش� ����ü�� �����Ÿ��� �����ϴ� �Լ�
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        // �ش� ����ü�� �����Ÿ��� �����ϴ� �Լ�
        this.moveSpeed = moveSpeed;
    }

    /* ��� �ּ� ó�� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if ((enemyHealth || indestructible || player) && !other.isTrigger)
        // ���� Ʈ���� �ݶ��̴��� �����Ѱ��� ��, �Ǵ� ���� �������� ������, Ʈ���Ű� �ƴ� �� �̶��..
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            // �÷��̾ ��źȯ�� �¾��� �� �Ǵ� ��źȯ�� ���� ���� ������ �ƴ϶��
            // ��, ���� �Ѿ��� �÷��̾ ������� �����ǰ� ��.
            {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation); // ���� ��ƼŬ
                Destroy(gameObject);
            }

            else if (!other.isTrigger && indestructible) // �����̳� ���� �Ұ����� ������Ʈ�� �ε�������
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation); // ���� ��ƼŬ
                Destroy(gameObject);
            }

        }
    }
    */

    private void DetectFireDistance()
    {
        // �����Ÿ��� ����� ȭ�� �ν��Ͻ��� ������Ű�� �޼���

        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        // ���� ��ġ�� ó�� ������ ��ġ���� �Ÿ��� �����Ÿ��� ��
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        // x�� ���� ����������� �̵�. 
    }
}
