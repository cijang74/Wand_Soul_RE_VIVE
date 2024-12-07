using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    private Transform target; // �÷��̾��� Transform ����
    private Animator animator; // �ִϸ��̼� ��� ���� Animator ������Ʈ
    private EnemyPathfinding enemyPathfinding; // ��� Ž���� ������Ʈ

    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private int skill1Chance = 2;    // ��ų1 Ȯ�� (2/10)
    [SerializeField] private int skill2Chance = 5;    // ��ų2 Ȯ�� (5/10)
    [SerializeField] private int skill3Chance = 3;    // ��ų3 Ȯ�� (3/10)


    private bool canUseSkill = true;

    public void Attack(){}
    public void Trace(){}
    public void Stop(){}
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
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

        // ��ų ��Ÿ�� ���
        yield return new WaitForSeconds(skillCoolDown);
        canUseSkill = true;
    }

    private void Skill1()
    {
        // ��ų1�� ���� ����
        Debug.Log("Skill 1 executed!");
        // ���⿡ �ִϸ��̼��̳� ���� ���� �߰�
    }

    private void Skill2()
    {
        // ��ų2�� ���� ����
        Debug.Log("Skill 2 executed!");
        // ���⿡ �ִϸ��̼��̳� ���� ���� �߰�
    }

    private void Skill3()
    {
        // ��ų3�� ���� ����
        Debug.Log("Skill 3 executed!");
        // ���⿡ �ִϸ��̼��̳� ���� ���� �߰�
    }

    private void Update()
    {
        if (canUseSkill)
        {
            StartCoroutine(UseRandomSkill());
        }
    }

}
