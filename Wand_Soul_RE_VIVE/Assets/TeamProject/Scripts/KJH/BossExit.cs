using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExit : MonoBehaviour
{
    private GameObject[] enemies;
    private OnExit onExit;
    private bool is_Dialog_End = false;

    private void Awake() 
    {
        onExit = FindObjectOfType<OnExit>();
    }

    void Start()
    {
        UpdateEnemyList();
        CheckAndDestroySelf();
    }

    void Update()
    {
        Debug.Log("적 더이상 없음 ");
        UpdateEnemyList();
        CheckAndDestroySelf();
    }

    public void UpdateEnemyList()
    {
        // Tag가 Enemy인 오브젝트들을 배열에 저장
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void CheckAndDestroySelf()
    {
        if (enemies.Length == 0 && is_Dialog_End)
        {
            Debug.Log("적 더이상 없음 ");
            onExit.ActiveExit();
        }
    }

    public void Set_is_Dialog_End()
    {
        is_Dialog_End = true;
    }
}
