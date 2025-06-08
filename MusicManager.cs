using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip; // 재생할 오디오 파일

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play(); // 씬이 로드되자마자 음악 재생
        }
    }
}