using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string startScene; // 불러올 씬
    private Vector3 originalScale; // 버튼의 원래 크기
    private NoInstanceFade noInstanceFade;
    public float shrinkScale = 0.9f; // 버튼이 작아질 크기 비율
    public float animationDuration = 0.2f; // 애니메이션 지속 시간
    private float waitRoLoadTime = 1f; // 씬 불러올때 시간 얼마나 줄것인지
    private bool startFlag = false;

    private void Awake() 
    {
        noInstanceFade = FindObjectOfType<NoInstanceFade>();
    }

    void Start()
    {
        originalScale = transform.localScale; // 버튼의 초기 크기 저장
    }

    public void OnPointerEnter(PointerEventData eventData) // 마우스 포인터가 해당 구역을 겹치면
    {
        Debug.Log("Pointer entered!");
        StopAllCoroutines(); // 진행 중인 애니메이션 중단
        StartCoroutine(ScaleTo(originalScale * shrinkScale)); // 크기 축소 애니메이션 시작
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스 포인터가 해당 구역을 나가면
    {
        Debug.Log("Pointer exited!");
        if(!startFlag)
        {
            StopAllCoroutines(); // 진행 중인 애니메이션 중단
            StartCoroutine(ScaleTo(originalScale)); // 원래 크기로 복원 애니메이션 시작
        }
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / animationDuration); // 크기를 점진적으로 변경
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // 최종 크기 설정
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitRoLoadTime);
        SceneManager.LoadScene(startScene); // 씬 불러오기
    }

    public void OnStartButtonClick()
    {
        startFlag = true;
        noInstanceFade.FadeToBlack(); // 페이드아웃
        StartCoroutine(LoadSceneRoutine());
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}