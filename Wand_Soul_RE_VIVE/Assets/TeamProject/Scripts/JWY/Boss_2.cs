using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : MonoBehaviour, IEnemy
{
    private Transform target; // �÷��̾��� Transform ����
    private Animator animator; // �ִϸ��̼� ��� ���� Animator ������Ʈ
    private EnemyPathfinding enemyPathfinding; // ��� Ž���� ������Ʈ
    private EnemyHealth enemyHealth;

    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private int skill1Chance = 5;    // ��ų1 Ȯ��
    [SerializeField] private int skill2Chance = 2;    // ��ų2 Ȯ��
    [SerializeField] private int skill3Chance = 3;    // ��ų3 Ȯ��

    [SerializeField] private GameObject iceSpikePrefab; // ��帧 ������
    [SerializeField] private int spikeCount = 7;        // ��帧 ����
    [SerializeField] private float horizontalSpacing = 2f; // ��帧 ����
    [SerializeField] private float spawnHeight = 10f;  // ��帧 ���� ����
    [SerializeField] private float dangerZoneDuration = 2f; // �� ǥ�� ���� �ð�

    [SerializeField] private GameObject bulletSpawnerPrefab; // ��ź ������ ������

    [SerializeField] private GameObject bossPrefab;
    private bool bossSpawned = false;

    private bool canUseSkill = true;

    public void Attack() { }
    public void Trace() { }
    public void Stop() { }

    public void ActiveOffBoss()
    {
        gameObject.SetActive(false);
    }

    public void ActiveBoss()
    {
        gameObject.SetActive(true);
    }

    //������ �̵��� ���ؼ� ���� �Ĵٺ��� �� �߰��߾��.
    private void FacePlayer()
    {
        if (target == null) return;

        // �÷��̾���� ���� ���
        Vector3 direction = (target.position - transform.position).normalized;

        // ���⿡ ���� ������ Scale�� �����Ͽ� Flip
        if (direction.x > 0)
        {
            // �������� �ٶ󺸵��� ����
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            // ������ �ٶ󺸵��� ����
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemyHealth = GetComponent<EnemyHealth>();

        // ���� ü���� �ִ� ü���� �������� ����
        if (enemyHealth != null)
        {
            int halfHealth = enemyHealth.GetMaxHealth() / 2; // �ִ� ü���� ���� ���
            enemyHealth.InitializeHealth(halfHealth); // ���� ü���� �������� ����
        }
    }

    private void Update()
    {
        if (canUseSkill)
        {
            StartCoroutine(UseRandomSkill());
        }
        // �÷��̾� �ٶ󺸱�
        FacePlayer();
    }

    private IEnumerator UseRandomSkill()
    {
        canUseSkill = false;

        int totalChance = skill1Chance + skill2Chance + skill3Chance;
        int randomSkill = Random.Range(0, totalChance);

        if (randomSkill < skill1Chance) // ��ų1
        {
            Skill1();
        }
        else if (randomSkill < skill1Chance + skill2Chance) // ��ų2
        {
            Skill2();
        }
        else // ��ų3
        {
            Skill3();
        }

        yield return new WaitForSeconds(skillCoolDown);
        canUseSkill = true;
    }

    private void Skill1()
    {
        Debug.Log("Skill 1 executed!");

        // 1. ���� ���� ǥ��
        StartCoroutine(DrawDangerZone());
    }

    private IEnumerator DrawDangerZone()
    {
        // ��帧 ��ġ ����� ����Ʈ
        List<Vector3> spikePositions = new List<Vector3>();

        float startX = target.position.x - (horizontalSpacing * (spikeCount - 1)) / 2;

        for (int i = 0; i < spikeCount; i++)
        {
            // LineRenderer�� �������� ����
            GameObject lineObject = new GameObject("LineRenderer_" + i);
            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            // LineRenderer �⺻ ����
            line.material = new Material(Shader.Find("Unlit/Color")); // Unlit Shader�� ����
            line.material.color = Color.red; // ���������� ����
            line.positionCount = 2; // �������� ����
            line.startWidth = 1f;
            line.endWidth = 1f;
            line.useWorldSpace = true;

            // ���� �������� ���� ����
            float x = startX + i * horizontalSpacing;
            Vector3 startPoint = new Vector3(x, target.position.y + spawnHeight, 0);
            Vector3 endPoint = new Vector3(x, target.position.y - 10, 0);

            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);

            // ��帧�� ������ ��ġ�� ����Ʈ�� �߰�
            spikePositions.Add(startPoint);

            // dangerZoneDuration �� LineRenderer ����
            Destroy(lineObject, dangerZoneDuration);
        }

        // ���� ǥ�� �ð� ���
        yield return new WaitForSeconds(dangerZoneDuration);

        // ���� ���� �� ��帧 ���� ȣ��
        StartCoroutine(SpawnVerticalIceSpikes(spikePositions));
    }

    private IEnumerator SpawnVerticalIceSpikes(List<Vector3> spikePositions)
    {
        // ��帧 ����
        foreach (var position in spikePositions)
        {
            GameObject spike = Instantiate(iceSpikePrefab, position, Quaternion.identity);

            // ��帧 �Ʒ��� �������� �ӵ� ����
            Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * 10f; // �Ʒ��� �������� �ӵ�
        }

        yield return null; // �ڷ�ƾ ����
    }

    private void Skill2()
    {
        Debug.Log("Skill 2 executed!");
        // ��ų2 ���� �߰�
        // ������ ���� ��ġ
        Vector3 leftSpawnPosition = new Vector3(target.position.x - 4f, target.position.y + 5f, 0);
        Vector3 rightSpawnPosition = new Vector3(target.position.x + 4f, target.position.y + 5f, 0);

        // ������ ������ ����
        GameObject leftSpawner = Instantiate(bulletSpawnerPrefab, leftSpawnPosition, Quaternion.identity);
        GameObject rightSpawner = Instantiate(bulletSpawnerPrefab, rightSpawnPosition, Quaternion.identity);

        // �� �����ʿ� �ʱ�ȭ ȣ��
        BulletSpawner leftBulletSpawner = leftSpawner.GetComponent<BulletSpawner>();
        BulletSpawner rightBulletSpawner = rightSpawner.GetComponent<BulletSpawner>();

        if (leftBulletSpawner != null)
        {
            leftBulletSpawner.Initialize(Vector3.left); // ���� ���� ������
        }

        if (rightBulletSpawner != null)
        {
            rightBulletSpawner.Initialize(Vector3.right); // ������ ���� ������
        }
    }

    private void Skill3()
    {
        Debug.Log("Skill 3 executed!");
        // ��ų3 ���� �߰�
        // 1. �����̵� ��ġ ����
        Vector3 teleportPosition = DetermineTeleportPosition();

        // 2. �����̵�
        StartCoroutine(TeleportAndCharge(teleportPosition));
    }

    private Vector3 DetermineTeleportPosition()
    {
        // �÷��̾��� ��ġ�� �������� ����
        float xOffset = Random.value > 0.5f ? 3f : -3f; // 50% Ȯ���� +3 �Ǵ� -3
        return new Vector3(target.position.x + xOffset, target.position.y, transform.position.z);
    }

    private IEnumerator TeleportAndCharge(Vector3 teleportPosition)
    {
        // �����̵�
        transform.position = teleportPosition;

        // ��� ���
        yield return new WaitForSeconds(1f);

        // �����ġ�� ����
        Vector3 chargeDirection = teleportPosition.x < target.position.x ? Vector3.right : Vector3.left;
        float chargeDistance = 12f;
        float chargeSpeed = 10f;

        Vector3 destination = transform.position + chargeDirection * chargeDistance;

        // �����ġ�� �̵�
        float elapsedTime = 0f;
        float chargeDuration = chargeDistance / chargeSpeed;

        while (elapsedTime < chargeDuration)
        {
            transform.position = Vector3.Lerp(transform.position, destination, elapsedTime / chargeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �浹 ������ ���� Collider �߰� ���� �ۼ� ����
        Debug.Log("Charge complete!");

        // �����ġ�� ���� �� ������ ���� �ൿ �غ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(2, transform); // ������ 2 ����
            }

            Debug.Log("Player hit by Boss charge!");
        }
    }

}
