using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInventory : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private Transform[] inventorySlots;
    private int slotCount = 5;
    private int inventoryCount = 8;

    private bool left = true;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        inventorySlots = new Transform[8];  //인벤토리 수만큼 배열

        for(int i = 0; i < inventoryCount; i++)
        {
            inventorySlots[i] = transform.GetChild(i);      //인벤토리에 있는 거 저장
        }

        playerControls.Inventory.RotateLeft.performed += _ => RotateInventory(left);    //Q 누르면 왼쪽으로 움직이기
        playerControls.Inventory.RotateRight.performed += _ => RotateInventory(!left);  //E 누르면 오른쪽으로 움직이기
    }

    private void OnEnable() 
    {
        playerControls.Enable();    
    }
    private void OnDisable() 
    {
        playerControls.Disable();
    }

    private void RotateInventory(bool left)      //왼쪽으로 움직이기
    {
        int[] castingSlots = ActiveWeapon.Instance.GetCastingSlotIndices();
        HashSet<int> castingSlotSet = new HashSet<int>(castingSlots);

        List<int> nonCastingSlots = new List<int>();
        for(int i = 0; i < inventoryCount; i++)
        {
            if(!castingSlotSet.Contains(i))
            {
                nonCastingSlots.Add(i);
            }
        }

        if(left)
        {
            InventorySlot firstSlot = inventorySlots[0].GetComponent<InventorySlot>();      //배열 0번 임시 저장
            WeaponInfo temp = firstSlot.GetWeaponInfo();

            for(int i = 0; i < nonCastingSlots.Count - 1; i++)
            {
                int currentSlot = nonCastingSlots[i];
                int nextSlot = nonCastingSlots[i + 1];

                inventorySlots[currentSlot].GetComponent<InventorySlot>().SetWeaponInfo(inventorySlots[nextSlot].GetComponent<InventorySlot>().GetWeaponInfo());     //오른쪽거를 왼쪽에 가져오기
            }

            inventorySlots[inventoryCount - 1].GetComponent<InventorySlot>().SetWeaponInfo(temp);    //임시 저장한거 맨 뒤에 복사
        }
        else
        {
            InventorySlot lastSlot = inventorySlots[inventoryCount - 1].GetComponent<InventorySlot>();   //배열 마지막꺼 임시 저장
            WeaponInfo temp = lastSlot.GetWeaponInfo();

            for(int i = nonCastingSlots.Count - 1; i > 0; i--)
            {
                int currentSlot = nonCastingSlots[i];
                int prevSlot = nonCastingSlots[i - 1];

                inventorySlots[currentSlot].GetComponent<InventorySlot>().SetWeaponInfo(inventorySlots[prevSlot].GetComponent<InventorySlot>().GetWeaponInfo());     //왼쪽거를 오른쪽에 가져오기
            }

            inventorySlots[0].GetComponent<InventorySlot>().SetWeaponInfo(temp);        //임시 저장한거 맨 앞에 복사
        }

        //UpdateActiveSlot();        //무기 활성화
    }

    private void UpdateActiveSlot()    //무기 활성화 시키기
    {
        int activeSlot = ActiveInventory.Instance.GetActiveSlotIndex();     //현재 활성화된 슬롯 번호 가져오기

        ActiveInventory.Instance.SetActiveSlotIndex(activeSlot);            //현재 활성화된 슬롯에 있는 무기 활성화시키기
    }

    public void MoveRuneBack(int slotIndex)
    {
        WeaponInfo completedWeapon = inventorySlots[slotIndex].GetComponent<InventorySlot>().GetWeaponInfo();
        //캐스팅 완료된 룬 저장

        //해당슬롯 뒤의 원소들을 한 칸씩 앞으로 이동
        for (int i = slotIndex; i < inventoryCount - 1; i++)
        {
            inventorySlots[i].GetComponent<InventorySlot>().SetWeaponInfo(inventorySlots[i + 1].GetComponent<InventorySlot>().GetWeaponInfo());
        }

        //캐스팅 완료된 룬을 맨 뒤로 이동
        inventorySlots[inventoryCount - 1].GetComponent<InventorySlot>().SetWeaponInfo(completedWeapon);
    }

    public Transform GetSlot(int index)
    {
        return inventorySlots[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
