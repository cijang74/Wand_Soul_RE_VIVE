using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderAttack : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject Thunder;
    private Vector3 ThunderSpawnPoint;

    private Animator myAnimator;
    readonly int Attack_HASH = Animator.StringToHash("Attack");

    private void Awake() 
    {
        myAnimator = GetComponent<Animator>();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        SpawnStaffProjectileAnimEvent();
    }

    private void SetSpawnPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        ThunderSpawnPoint.x = mousePosition.x;
        ThunderSpawnPoint.y = mousePosition.y + 20f;
        ThunderSpawnPoint.z = 0f;
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        // 애니메이션에서 실행시킬 함수

        SetSpawnPoint();

        GameObject newLazer = Instantiate(Thunder, ThunderSpawnPoint, Quaternion.identity);
        // 레이저의 인스턴스화
        newLazer.GetComponent<ThunderLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    private void MouseFollowWithOffset() // 마우스 포인터 위치에 따라 무기 방향 전환
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        // 카메라 기준으로의 플레이어의 위치값을 선정하여 저장

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; 
        // y를 x로 나눈 탄젠트. 0~6값을 2라디안으로 변경

        if(mousePos.x < playerScreenPoint.x) // 마우스 x좌표값이 플레이어의x좌표값보다 작다면
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
            // 쿼터니언 이용하여 뒤집고, 각도 변경
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    private void Update() 
    {
        MouseFollowWithOffset();
    }
}
