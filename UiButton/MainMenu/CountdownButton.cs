using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownButton : MonoBehaviour
{
    public Button playButton;
    public Button countdownButton;
    public Text countdownText;
    public float countdownTime = 5f;
    public AudioSource countdownAudio; // 카운트다운 소리용 AudioSource
    public AudioSource buttonAudioSource; // 버튼 소리용 AudioSource
    public float volumeMultiplier = 10f; // 버튼 소리 크기 배수 (기본은 1)

    private Coroutine _countdownCoroutine;
    private bool _isCountingDown = false;

    void Start()
    {
        playButton.onClick.AddListener(StartCountdown);
        countdownButton.onClick.AddListener(OnCountdownButtonClicked);
        countdownButton.gameObject.SetActive(false);
    }

    void StartCountdown()
    {
        // 버튼 소리 크기 키우기
        if (buttonAudioSource != null)
        {
            buttonAudioSource.volume = Mathf.Clamp01(volumeMultiplier); // 최대 1까지 제한
            buttonAudioSource.Play(); // 버튼 소리 재생
        }

        // 카운트다운 소리 볼륨 설정
        if (countdownAudio != null)
        {
            countdownAudio.volume = 5f; // 카운트다운 소리 최대 볼륨
            countdownAudio.Play(); // 카운트다운 소리 재생
        }

        countdownButton.gameObject.SetActive(true);
        countdownText.text = countdownTime.ToString("F0") + "초 남음";

        if (!_isCountingDown)
        {
            _countdownCoroutine = StartCoroutine(CountdownRoutine());
        }
    }

    void OnCountdownButtonClicked()
    {
        if (_isCountingDown)
        {
            StopCoroutine(_countdownCoroutine);
            countdownText.text = "00:00";
            _isCountingDown = false;

            if (countdownAudio != null)
                countdownAudio.Stop(); // 카운트다운 소리 정지

            StartCoroutine(HideButtonAfterDelay());
        }
    }

    // 버튼 숨기기 전에 딜레이를 주는 코루틴
    IEnumerator HideButtonAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        countdownButton.gameObject.SetActive(false);
    }

    IEnumerator CountdownRoutine()
    {
        _isCountingDown = true;
        float timeLeft = countdownTime;

        while (timeLeft > 0f)
        {
            int seconds = Mathf.FloorToInt(timeLeft);
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;

            countdownText.text = string.Format("{0:00}:{1:00}", minutes, remainingSeconds);

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        countdownText.text = "START!";
        _isCountingDown = false;

        if (countdownAudio != null)
            countdownAudio.Stop(); // 카운트다운 소리 정지

        // 씬 전환
        SceneManager.LoadScene("ChoesST");
    }
}
