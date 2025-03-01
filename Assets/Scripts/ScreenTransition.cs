using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition instance;

    [Header("�������")]
    public GameObject BlackPanel; // �������

    [Header("����ʱ��")]
    public float fadeDuration = 1f; // ����ʱ��

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

        // ȷ�� BlackPanel ���� null
        if (BlackPanel == null)
        {
            Debug.LogError("ScreenTransition: BlackPanel δ���ã�");
            return;
        }

        // ��ȡ����� CanvasGroup ���
        canvasGroup = BlackPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = BlackPanel.AddComponent<CanvasGroup>();

        // ȷ����ʼʱ�������ɼ�
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        BlackPanel.SetActive(true); // ��������壬ֻ������͸��
    }

    // �������루��ڣ�
    public void FadeToBlack()
    {
        StartCoroutine(FadeIn());
    }

    // �����������ָ����棩
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
            // ʹ�� Mathf.Lerp ��ȷ�����Խ���
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1; // ȷ��������ȫ���
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // ʹ�� Mathf.Lerp ��ȷ�����Խ���
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0; // ȷ����ȫ�ָ�
    }

    public void StartBlackScreenTransition()
    {
        StartCoroutine(BlackScreenTransition());
    }

    private IEnumerator BlackScreenTransition()
    {
        yield return StartCoroutine(FadeIn());  // ��������
        yield return new WaitForSeconds(0.5f);
        // ͣ������һ��ʱ��
        yield return StartCoroutine(FadeOut()); // ����ָ�
    }
}
