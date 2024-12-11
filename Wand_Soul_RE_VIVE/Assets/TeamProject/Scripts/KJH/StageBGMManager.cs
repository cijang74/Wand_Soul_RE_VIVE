using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip music;

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        AudioSourceManager.Instance.PlayBGM(music);
    }
}
