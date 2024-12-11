using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject[] enemies;

    void Start()
    {
        UpdateEnemyList();
        CheckAndDestroySelf();
    }

    void Update()
    {
        UpdateEnemyList();
        CheckAndDestroySelf();
    }

    private void UpdateEnemyList()
    {
        // Tag가 Enemy인 오브젝트들을 배열에 저장
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void CheckAndDestroySelf()
    {
        if (enemies.Length == 0)
        {
            Debug.Log("적 더이상 없음 ");
            Destroy(gameObject);
        }
    }
}
