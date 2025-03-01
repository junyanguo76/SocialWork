using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition instance;

    [Header("黑屏面板")]
    public GameObject BlackPanel; // 黑屏面板

    [Header("过渡时间")]
    public float fadeDuration = 1f; // 过渡时间

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 确保 BlackPanel 不是 null
        if (BlackPanel == null)
        {
            Debug.LogError("ScreenTransition: BlackPanel 未设置！");
            return;
        }

        // 获取或添加 CanvasGroup 组件
        canvasGroup = BlackPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = BlackPanel.AddComponent<CanvasGroup>();

        // 确保初始时黑屏不可见
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        BlackPanel.SetActive(true); // 不隐藏面板，只是让它透明
    }

    // 黑屏淡入（变黑）
    public void FadeToBlack()
    {
        StartCoroutine(FadeIn());
    }

    // 黑屏淡出（恢复画面）
    public void FadeFromBlack()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 使用 Mathf.Lerp 来确保线性渐变
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1; // 确保最终完全变黑
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 使用 Mathf.Lerp 来确保线性渐变
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0; // 确保完全恢复
    }

    public void StartBlackScreenTransition()
    {
        StartCoroutine(BlackScreenTransition());
    }

    private IEnumerator BlackScreenTransition()
    {
        yield return StartCoroutine(FadeIn());  // 黑屏渐变
        yield return new WaitForSeconds(0.5f);
        // 停留黑屏一段时间
        yield return StartCoroutine(FadeOut()); // 画面恢复
    }
}
