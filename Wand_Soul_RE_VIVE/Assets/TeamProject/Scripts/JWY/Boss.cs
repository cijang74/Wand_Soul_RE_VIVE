using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    private Transform target; // 플레이어의 Transform 정보
    private Animator animator; // 애니메이션 제어를 위한 Animator 컴포넌트
    private EnemyPathfinding enemyPathfinding; // 경로 탐색용 컴포넌트

    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private int skill1Chance = 2;    // 스킬1 확률 (2/10)
    [SerializeField] private int skill2Chance = 5;    // 스킬2 확률 (5/10)
    [SerializeField] private int skill3Chance = 3;    // 스킬3 확률 (3/10)


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

        if (randomSkill < skill1Chance) // 스킬1
        {
            Skill1();
        }
        else if (randomSkill < skill1Chance + skill2Chance) // 스킬2
        {
            Skill2();
        }
        else // 스킬3
        {
            Skill3();
        }

        // 스킬 쿨타임 대기
        yield return new WaitForSeconds(skillCoolDown);
        canUseSkill = true;
    }

    private void Skill1()
    {
        // 스킬1의 로직 구현
        Debug.Log("Skill 1 executed!");
        // 여기에 애니메이션이나 공격 로직 추가
    }

    private void Skill2()
    {
        // 스킬2의 로직 구현
        Debug.Log("Skill 2 executed!");
        // 여기에 애니메이션이나 공격 로직 추가
    }

    private void Skill3()
    {
        // 스킬3의 로직 구현
        Debug.Log("Skill 3 executed!");
        // 여기에 애니메이션이나 공격 로직 추가
    }

    private void Update()
    {
        if (canUseSkill)
        {
            StartCoroutine(UseRandomSkill());
        }
    }

}
