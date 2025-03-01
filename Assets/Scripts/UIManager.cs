using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject settingPanel;

    public Slider kindenessBar;
    public Slider inteligenceBar;
    public Slider complianceBar;

    public GameObject BlackPanel;
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

    public void ChangeVaule(int kindnessValue,int intellegenceValue,int complianceValue)
    {
        kindenessBar.value += kindnessValue;
        inteligenceBar.value += intellegenceValue;
        complianceBar.value += complianceValue; 
    }

}
