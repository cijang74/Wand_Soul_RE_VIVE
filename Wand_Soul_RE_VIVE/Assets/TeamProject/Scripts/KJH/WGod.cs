using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGod : MonoBehaviour
{

    private TextEvent textEvent;

    public void DestroyHer()
    {
        Destroy(gameObject);
    }

    public void HealPlayer() // 플레이어를 모두 회복해준다. (5골드)
    {
        PlayerHealth.Instance.AllHealPlayer();
    }

    public void UpSeedPlayer() // 플레이어의 스피드를 2증가시켜준다. (8골드)
    {
        PlayerController.Instance.ImproveMovementSpeed();
    }

    public void UpStaminaRefresh() // 플레이어의 대쉬 회복 속도를 1초 줄여준다. (15골드)
    {
        Stamina.Instance.DownRefreshTime();
    }

    public void UpPlayerMaxHP() // 플레이어의 최대 체력을 1증가시켜준다. (20골드)
    {
        PlayerHealth.Instance.UpMaxHP();
    }
}
