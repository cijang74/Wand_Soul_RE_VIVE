using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGod : MonoBehaviour
{
    [SerializeField] private TextEvent textEvent;
    [SerializeField] private GameObject EndVFXPrefab;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            textEvent.ShowStartText();
        }
    }

    public void DestroyHer()
    {
        Instantiate(EndVFXPrefab, transform.position, Quaternion.identity); // 해당 위치로 인스턴스화
        Destroy(gameObject);
    }

    public void HealPlayer() // 플레이어를 모두 회복해준다. (5골드)
    {
        if(EconomyManager.Instance.currentGold >= 5)
        {
            PlayerHealth.Instance.AllHealPlayer();
            EconomyManager.Instance.UpdateMinusCoin(5);
        }

        else
        {
            textEvent.NoCoinText();
        }
    }

    public void UpSeedPlayer() // 플레이어의 스피드를 2증가시켜준다. (8골드)
    {
        if(EconomyManager.Instance.currentGold >= 8)
        {
            PlayerController.Instance.ImproveMovementSpeed();
            EconomyManager.Instance.UpdateMinusCoin(5);
        }

        else
        {
            textEvent.NoCoinText();
        }
    }

    public void UpStaminaRefresh() // 플레이어의 대쉬 회복 속도를 1초 줄여준다. (15골드)
    {
        if(EconomyManager.Instance.currentGold >= 15)
        {
            Stamina.Instance.DownRefreshTime();
            EconomyManager.Instance.UpdateMinusCoin(5);
        }

        else
        {
            textEvent.NoCoinText();
        }
    }

    public void UpPlayerMaxHP() // 플레이어의 최대 체력을 1증가시켜준다. (20골드)
    {
        if(EconomyManager.Instance.currentGold >= 20)
        {
            PlayerHealth.Instance.UpMaxHP();
            EconomyManager.Instance.UpdateMinusCoin(5);
        }

        else
        {
            textEvent.NoCoinText();
        }
    }
}
