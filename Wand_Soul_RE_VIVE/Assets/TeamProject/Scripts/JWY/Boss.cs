using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    private Transform target; // �÷��̾��� Transform ����
    private Animator animator; // �ִϸ��̼� ��� ���� Animator ������Ʈ
    private EnemyPathfinding enemyPathfinding; // ��� Ž���� ������Ʈ

    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private int skill1Chance = 5;    // ��ų1 Ȯ��
    [SerializeField] private int skill2Chance = 2;    // ��ų2 Ȯ��
    [SerializeField] private int skill3Chance = 3;    // ��ų3 Ȯ��

    [SerializeField] private GameObject iceSpikePrefab; // ��帧 ������
    [SerializeField] private int spikeCount = 7;        // ��帧 ����
    [SerializeField] private float horizontalSpacing = 2f; // ��帧 ����
    [SerializeField] private float spawnHeight = 10f;  // ��帧 ���� ����
    [SerializeField] private float dangerZoneDuration = 2f; // �� ǥ�� ���� �ð�

    private bool canUseSkill = true;

    public void Attack() { }
    public void Trace() { }
    public void Stop() { }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Update()
    {
        if (canUseSkill)
        {
            StartCoroutine(UseRandomSkill());
        }
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
            rb.velocity = Vector2.down * 5f; // �Ʒ��� �������� �ӵ�
        }

        yield return null; // �ڷ�ƾ ����
    }

    private void Skill2()
    {
        Debug.Log("Skill 2 executed!");
        // ��ų2 ���� �߰�
    }

    private void Skill3()
    {
        Debug.Log("Skill 3 executed!");
        // ��ų3 ���� �߰�
    }
}
