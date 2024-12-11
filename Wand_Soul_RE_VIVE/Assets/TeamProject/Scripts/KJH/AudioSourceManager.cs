using UnityEngine;

public class AudioSourceManager : Singleton<AudioSourceManager>
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip music; // Track 4 BGM

    protected override void Awake() 
    {
        base.Awake();
    }

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        PlayBGM(music);
    }
    
    public void PlayBGM(AudioClip newBGM)
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // 기존 BGM 정지
            audioSource.clip = newBGM; // 새로운 BGM 설정
            audioSource.Play(); // 새로운 BGM 재생
        }
    }

    public void PlaySFX(AudioClip newBGM)
    {
        if (audioSource != null)
        {
            audioSource.clip = newBGM; // 새로운 BGM 설정
            audioSource.Play(); // 새로운 BGM 재생
        }
    }
}
