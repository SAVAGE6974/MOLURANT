using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutBackground : MonoBehaviour
{
    public CanvasGroup blackBackground;  // 검정 배경을 위한 CanvasGroup
    public float fadeDuration = 1f;       // 페이드 아웃 시간 (5초)

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float startAlpha = blackBackground.alpha;
        float endAlpha = 0f; // 완전히 투명하게 만들기
        float elapsedTime = 0f;

        // 서서히 alpha 값을 0으로 바꾸는 코루틴
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackBackground.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null; // 매 프레임마다 실행
        }

        // 최종적으로 완전히 투명한 상태로 설정
        blackBackground.alpha = endAlpha;
    }
}