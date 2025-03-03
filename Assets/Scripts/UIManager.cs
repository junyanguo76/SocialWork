using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject settingPanel;

    public Slider kindenessBar;
    public Slider inteligenceBar;
    public Slider complianceBar;

    public GameObject blackPanel;
    public UIFadeInOut barsPanel;



    // Start is called before the first frame update
    void Start()
    {
        kindenessBar.value = 15;
        inteligenceBar.value = 15;
        complianceBar.value = 15;
        instance = this;
    }


    public void OpenSetting()
    {
        if(settingPanel.activeInHierarchy == true)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            settingPanel.SetActive(true);
        }   
    }
    public void Back()
    {
        settingPanel.SetActive(false);
    }

    public void ChangeValue(int kindnessValue, int intelligenceValue, int complianceValue)
    {
        barsPanel.StartFade();

        UpdateBar(kindenessBar, kindnessValue);
        UpdateBar(inteligenceBar, intelligenceValue);
        UpdateBar(complianceBar, complianceValue);
    }

    private void UpdateBar(Slider bar, int value)
    {
        if (bar == null) return;

        TMP_Text text = bar.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            if (value == 0)
            {
                text.text = ""; // 不显示文本
            }
            else
            {
                text.text = value > 0 ? "+" + value : value.ToString();

                // 设置颜色
                Color color;
                if (value > 0)
                {
                    ColorUtility.TryParseHtmlString("#18C400", out color); // 绿色
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#AE1028", out color); // 红色
                }
                text.color = color;
            }
        }

        bar.value += value;
    }




}
