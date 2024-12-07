using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    private Transform target; // 플레이어의 Transform 정보
    private Animator animator; // 애니메이션 제어를 위한 Animator 컴포넌트
    private EnemyPathfinding enemyPathfinding; // 경로 탐색용 컴포넌트

    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private int skill1Chance = 5;    // 스킬1 확률
    [SerializeField] private int skill2Chance = 2;    // 스킬2 확률
    [SerializeField] private int skill3Chance = 3;    // 스킬3 확률

    [SerializeField] private GameObject iceSpikePrefab; // 고드름 프리팹
    [SerializeField] private int spikeCount = 7;        // 고드름 개수
    [SerializeField] private float horizontalSpacing = 2f; // 고드름 간격
    [SerializeField] private float spawnHeight = 10f;  // 고드름 생성 높이
    [SerializeField] private float dangerZoneDuration = 2f; // 선 표시 지속 시간

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

        yield return new WaitForSeconds(skillCoolDown);
        canUseSkill = true;
    }

    private void Skill1()
    {
        Debug.Log("Skill 1 executed!");

        // 1. 위험 영역 표시
        StartCoroutine(DrawDangerZone());
    }

    private IEnumerator DrawDangerZone()
    {
        // 고드름 위치 저장용 리스트
        List<Vector3> spikePositions = new List<Vector3>();

        float startX = target.position.x - (horizontalSpacing * (spikeCount - 1)) / 2;

        for (int i = 0; i < spikeCount; i++)
        {
            // LineRenderer를 동적으로 생성
            GameObject lineObject = new GameObject("LineRenderer_" + i);
            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            // LineRenderer 기본 설정
            line.material = new Material(Shader.Find("Unlit/Color")); // Unlit Shader로 설정
            line.material.color = Color.red; // 빨간색으로 설정
            line.positionCount = 2; // 시작점과 끝점
            line.startWidth = 1f;
            line.endWidth = 1f;
            line.useWorldSpace = true;

            // 라인 시작점과 끝점 설정
            float x = startX + i * horizontalSpacing;
            Vector3 startPoint = new Vector3(x, target.position.y + spawnHeight, 0);
            Vector3 endPoint = new Vector3(x, target.position.y - 10, 0);

            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);

            // 고드름이 생성될 위치를 리스트에 추가
            spikePositions.Add(startPoint);

            // dangerZoneDuration 후 LineRenderer 제거
            Destroy(lineObject, dangerZoneDuration);
        }

        // 라인 표시 시간 대기
        yield return new WaitForSeconds(dangerZoneDuration);

        // 라인 제거 후 고드름 생성 호출
        StartCoroutine(SpawnVerticalIceSpikes(spikePositions));
    }

    private IEnumerator SpawnVerticalIceSpikes(List<Vector3> spikePositions)
    {
        // 고드름 생성
        foreach (var position in spikePositions)
        {
            GameObject spike = Instantiate(iceSpikePrefab, position, Quaternion.identity);

            // 고드름 아래로 떨어지는 속도 설정
            Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * 5f; // 아래로 떨어지는 속도
        }

        yield return null; // 코루틴 종료
    }

    private void Skill2()
    {
        Debug.Log("Skill 2 executed!");
        // 스킬2 로직 추가
    }

    private void Skill3()
    {
        Debug.Log("Skill 3 executed!");
        // 스킬3 로직 추가
    }
}
