using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSound; // 버튼 클릭 사운드
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}