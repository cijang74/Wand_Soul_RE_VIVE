using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // 스크립터블 오브젝트 WeaponInfo의 정보를 다른 스크립트에서 접근할 수 있도록 하는 클래스
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private Image icon;

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SetWeaponInfo(WeaponInfo newWeaponInfo)
    {
        weaponInfo = newWeaponInfo;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (icon == null)
        {
            Debug.LogError("InventorySlot: 'icon' is not assigned in the Inspector!");
            return;
        }

        if (weaponInfo != null)
        {
            icon.sprite = weaponInfo.weaponIcon;
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }
    }
}
