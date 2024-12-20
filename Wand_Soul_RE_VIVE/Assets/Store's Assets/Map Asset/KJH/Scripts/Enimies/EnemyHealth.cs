﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockbackThrust = 15f; // 넉벡량

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;
    private Boss boss;
    private Boss_2 boss_2;
    private Slider heathSlider;
    private Slider heathSlider2;

    const string  BOSS_HEALTH_SLIDER_TEXT= "Boss Health Slider";
    const string  BOSS_SHADOW_HEALTH_SLIDER_TEXT= "Boss Shadow Health Slider";

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        boss = GetComponent<Boss>();
        boss_2 = GetComponent<Boss_2>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        // 데미지를 입었을 때 실행되는 함수
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockbackThrust); // 넉벡시키기
        StartCoroutine(flash.FlashRoutine()); // Flash시키기
        UpdateHealthSlider();
        StartCoroutine(CheckDetectorDeathRoutime()); // 죽는피인지 검사하기
    }

    private IEnumerator CheckDetectorDeathRoutime()
    {
        // GetRestoreMatTime초 있다가 삭제하기 위해 만들어놓은 코루틴
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath()
    {
        // 죽음을 검사하고 죽을때 실행되는 함수
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity); // 해당 위치로 인스턴스화
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return startingHealth;
    }

    public void InitializeHealth(int health)
    {
        currentHealth = health;
    }

    private void UpdateHealthSlider()
    // 체력 UI 연결하여 업데이트
    {
        if(boss != null)
        {
            if(heathSlider == null)
            {
                heathSlider = GameObject.Find(BOSS_HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            }

            heathSlider.maxValue = startingHealth;
            heathSlider.value = currentHealth;
        }

        if(boss_2 != null)
        {
            if(heathSlider2 == null)
            {
                heathSlider2 = GameObject.Find(BOSS_SHADOW_HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            }

            heathSlider2.maxValue = startingHealth;
            heathSlider2.value = currentHealth;
        }
    }
}
