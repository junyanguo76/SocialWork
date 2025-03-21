using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject CityPanel;
    public GameObject HousePanel;
    public GameObject OfficePanel;


    // 按钮数组，按顺序分别为 case1、case2、case3
    public Button[] buttons;
    // CSV 文件数组（以 TextAsset 导入），顺序与按钮对应
    public TextAsset[] csvFiles;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        RegisterButtons();
    }

    void RegisterButtons()
    {
        int count = Mathf.Min(buttons.Length, csvFiles.Length);
        for (int i = 0; i < count; i++)
        {
            SetupButton(buttons[i], csvFiles[i]);
        }
    }

    // 封装单个按钮的注册方法
    void SetupButton(Button button, TextAsset csv)
    {
        button.onClick.AddListener(() => {
            DialogManager.instance.StartAStory(csv);
        });
    }


    public void JumpToNewScene(string sceneName)
    {

        StartCoroutine(WaitForOneSecond(sceneName));
        ScreenTransition.instance.StartBlackScreenTransition();

    }

    private IEnumerator WaitForOneSecond(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        CityPanel.SetActive(false);
        OfficePanel.SetActive(false);
        HousePanel.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        switch (sceneName)
        {
            case "Office": OfficePanel.SetActive(true); break;
            case "City":CityPanel.SetActive(true); break;
            case "House": HousePanel.SetActive(true); break;
        }

    }
}
