using System.Collections;
using UnityEngine;

public class UIFadeInOut : MonoBehaviour
{
    public float fadeDuration = 2f; // 渐变时间
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // 如果没有，添加 CanvasGroup 组件
        }
    }

    public void StartFade() // 外部调用这个方法触发渐变
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(2f); // 持续2秒
        yield return StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }
    }
}
