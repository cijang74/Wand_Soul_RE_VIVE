using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    // 무기 객체에 접근하여 해당 무기 스크립트를 읽어와 IWeapon인터페이스를 구현시켜주는 스크립트

    public MonoBehaviour CurrentLeftActiveWeapon{ get; private set; }
    public MonoBehaviour CurrentRightActiveWeapon{ get; private set; }
    // 활성화시킬 무기스크립트를 입력받는다
    private RuneInventory runeInventory;
    private PlayerControls playerControls;

    private float leftTimeToCast, rightTimeToCast; //공격 캐스팅 시간 변수
    private float leftCastingTimer, rightCastingTimer = 0f;
    
    private bool leftAttackButtonDown, rightAttackButtonDown, isLeftCasting, isRightCasting, leftCastingComplete, rightCastingComplete = false;

    private int leftClick = 1;
    private int rightClick = 3;


    [SerializeField] Image leftCastingCircle;
    [SerializeField] Image rightCastingCircle;


    protected override void Awake() 
    {
        base.Awake();
        playerControls = new PlayerControls();
        runeInventory = FindObjectOfType<RuneInventory>();
    }

    private void OnEnable() // 플레이어 컨트롤을 활성화 할 때 사용
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        // 마우스 왼쪽 클릭 시 시작이 수행됨
        // =>: 람다식, 연산자 왼쪽이 파라미터, 연산자 오른쪽이 실행문장
        // 즉, _(전달값 X)를 파라미터로하여 연산자 뒤 함수를 실행한 값을 덧붙여준다.
        playerControls.Combat.Attack.started += _ => 
        {
            ActivateWeaponBySlot(leftClick);
            StartCasting(leftClick);
        };  //좌클릭 공격 시작

        playerControls.Combat.Attack.canceled += _ => StartAttack(leftClick);      //좌클릭 공격 멈춤

        playerControls.Combat.Attack2.started += _ => 
        {
            ActivateWeaponBySlot(rightClick);
            StartCasting(rightClick);
        };  //우클릭 공격 시작
        
        playerControls.Combat.Attack2.canceled += _ => StartAttack(rightClick);     //우클릭 공격 멈춤

        //AttackCoolDown();
        //시작하자마자 공격버튼 못누르게 쿨타임 줌
    }

    public void ActivateWeaponBySlot(int slotIndex)
    {
        InventorySlot selectedSlot = runeInventory.GetSlot(slotIndex).GetComponent<InventorySlot>();
        WeaponInfo weaponInfo = selectedSlot.GetWeaponInfo();

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, transform.position, Quaternion.identity);
        newWeapon.transform.parent = this.transform;

        NewWeapon(newWeapon.GetComponent<MonoBehaviour>(), slotIndex);
    }

    public void NewWeapon(MonoBehaviour newWeapon, int slotIndex)
    {
        if(slotIndex == leftClick)
        {
            CurrentLeftActiveWeapon = newWeapon;
            // 무기를 바꿀때 기존 선택된 무기 오브젝트를 삭제하고 새로 만들때 사용할 함수

            leftTimeToCast = (CurrentLeftActiveWeapon as IWeapon).GetWeaponInfo().weaponCastingTime;
            // CurrentActiveWeapon 변수를 IWeapon형식으로 형변환 시킨다.
        }

        else if(slotIndex == rightClick)
        {
            CurrentRightActiveWeapon = newWeapon;

            rightTimeToCast = (CurrentRightActiveWeapon as IWeapon).GetWeaponInfo().weaponCastingTime;
        }
        
        //AttackCoolDown();   
        // 무기 사이를 왔다갔다 하면서 쿨타임 없애는거 방지하기 위해 현재 장착 무기의 쿨타임 변수만큼 쿨타임 줌                => 어차피 이제 캐스팅 해야해서 없어도 될 듯
        
        
    }

    public int[] GetCastingSlotIndices()
    {
        List<int> castingIndices = new List<int>();

        if(isLeftCasting)
        {
            castingIndices.Add(leftClick);
        }
        if(isRightCasting)
        {
            castingIndices.Add(rightClick);
        }

        return castingIndices.ToArray();
    }

    private void AttackCoolDown()                                                                                       //=> 쿨타임을 캐스팅 시간으로 대체해서 없어도 될듯
    {
        isLeftCasting = true;
        StopAllCoroutines(); // 해당 스크립트에 구현된 코루틴중 실행되고 있는 모든 코루틴 비활성화(안전용)
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()                                                                     //=> 쿨타임을 캐스팅 시간으로 대체해서 없어도 될듯
    {
        // 공격 쿨타임 가지는 함수
        yield return new WaitForSeconds(leftTimeToCast);
        isLeftCasting = false;
    }

    private void StartCasting(int slotIndex)
    {
        if(slotIndex == leftClick)
        {
            leftAttackButtonDown = true;
            isLeftCasting = true;
            leftCastingComplete = false;
            leftCastingTimer = 0f;

            if(leftCastingCircle != null)
            {
                leftCastingCircle.fillAmount = 0f;
                leftCastingCircle.gameObject.SetActive(true);
            }
        }
        else if(slotIndex == rightClick)
        {
            rightAttackButtonDown = true;
            isRightCasting = true;
            rightCastingComplete = false;
            rightCastingTimer = 0f;

            if(rightCastingCircle != null)
            {
                rightCastingCircle.fillAmount = 0f;
                rightCastingCircle.gameObject.SetActive(true);
            }
        }
    }

    private void StartAttack(int slotIndex)
    {
        if(slotIndex == leftClick)
        {
            isLeftCasting = false;
            //클릭 땠으니 시전중 x

            if(leftCastingTimer >= leftTimeToCast)          //캐스팅 시간보다 오래 눌렀으면
            {
                leftCastingComplete = true;                 //캐스팅 완료(시전 준비 완료)
            }
            
            if(leftCastingCircle != null)
            {
                leftCastingCircle.gameObject.SetActive(false);      //타이머 비활성화
            }
        }

        else if(slotIndex == rightClick)
        {
            isRightCasting = false;

            if(rightCastingTimer >= rightTimeToCast)          //캐스팅 시간보다 오래 눌렀으면
            {
                rightCastingComplete = true;
            }
            
            if(rightCastingCircle != null)
            {
                rightCastingCircle.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateUI()
    {
        if (isLeftCasting && !leftCastingComplete)      //누르는 중일때
        {
            float weaponCastingTime = (CurrentLeftActiveWeapon as IWeapon).GetWeaponInfo().weaponCastingTime;
            leftCastingTimer += Time.deltaTime;

            if(leftCastingCircle != null)
            {
                leftCastingCircle.fillAmount = Mathf.Clamp01(leftCastingTimer / weaponCastingTime);
                //안에 있는 값이 0보다 작으면 0, 1보다 크면 1, 그 사이면 그대로 리턴
            }
        }
        if (isRightCasting && !rightCastingComplete)      //누르는 중일때
        {
            float weaponCastingTime = (CurrentRightActiveWeapon as IWeapon).GetWeaponInfo().weaponCastingTime;
            rightCastingTimer += Time.deltaTime;

            if(rightCastingCircle != null)
            {
                rightCastingCircle.fillAmount = Mathf.Clamp01(rightCastingTimer / weaponCastingTime);
                //안에 있는 값이 0보다 작으면 0, 1보다 크면 1, 그 사이면 그대로 리턴
            }
        }
    }

    private void Update()
    {
        UpdateUI();
        Attack();
    }

    private void Attack()
    {
        if(leftAttackButtonDown && !isLeftCasting && CurrentLeftActiveWeapon)
        // 쿨다운상태거나 지금 무기를 들고있지 않으면 실행안됨                                  => 시전 중이거나 무기를 들고있지 않으면으로 변경하면 될듯
        {
            //AttackCoolDown();

            if(leftCastingComplete)
            {
                leftAttackButtonDown = false;
                leftCastingComplete = false;
                (CurrentLeftActiveWeapon as IWeapon).Attack();
                // as: 형변환, currentActiveWeapon스크립트가 IWeapon인터페이스에 있는
                // 함수를 구현하였다면 currentActiveWeapon스크립트의 Attack()메서드를 실행시킨다.

                runeInventory.MoveRuneBack(leftClick);
            }
        }
        if(rightAttackButtonDown && !isRightCasting && CurrentRightActiveWeapon)
        // 쿨다운상태거나 지금 무기를 들고있지 않으면 실행안됨                                  => 시전 중이거나 무기를 들고있지 않으면으로 변경하면 될듯
        {
            //AttackCoolDown();

            if(rightCastingComplete)
            {
                rightAttackButtonDown = false;
                rightCastingComplete = false;
                (CurrentRightActiveWeapon as IWeapon).Attack();
                // as: 형변환, currentActiveWeapon스크립트가 IWeapon인터페이스에 있는
                // 함수를 구현하였다면 currentActiveWeapon스크립트의 Attack()메서드를 실행시킨다.
                
                runeInventory.MoveRuneBack(rightClick);
            }
        }
    }
}

