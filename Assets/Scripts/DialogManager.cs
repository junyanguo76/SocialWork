using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Image character_left;
    public Image character_right;
    public GameObject dialogBox_left;
    public GameObject dialogBox_right;
    public TMP_Text nameText_left;
    public TMP_Text nameText_right;

    public TMP_Text storyText;

    public GameObject storyNextButton;
    public GameObject dialogNextButton;
    public GameObject storySelectionButton;
    public Transform storyButtonGroup;
    public GameObject dialogSelectionButton;
    public Transform dialogButtonGroup;

    public List<Sprite> sprites = new List<Sprite>();
    Dictionary<string,Sprite> imageDic = new Dictionary<string,Sprite>();

    public GameObject caseBoard;
    public GameObject dialogSystem;

    public int targetDialogID;
    public string[] dialogRows;

    public static DialogManager instance;
    private void Awake()
    {
        instance = this;
        imageDic["Empty"] = sprites[0];
        imageDic["Me"] = sprites[1];
        imageDic["Mark"] = sprites[2];
        imageDic["Lily"] = sprites[3];

    }

    public void UpdateStory(string _storyText)
    {
        if (_storyText != null)
        {
            storyText.text = _storyText;
        }
    }
    public void UpdateDialog(string _name,string _position, string _characterText)
    {
       if(_position == "l")
        {
            dialogBox_left.SetActive(true);
            dialogBox_right.SetActive(false);
            dialogBox_left.GetComponentInChildren<TMP_Text>().text = _characterText;

            if(_name != null)
            {
                character_left.sprite = imageDic[_name];
                nameText_left.text = _name;
            }
        }
        else if (_position == "r")
        {
            dialogBox_left.SetActive(false);
            dialogBox_right.SetActive(true);
            dialogBox_right.GetComponentInChildren<TMP_Text>().text = _characterText;

            if (_name != null)
            {
                character_right.sprite = imageDic[_name];
                nameText_right.text = _name;
            }
        }
    }

    public void StartAStory(UnityEngine.TextAsset _asset)
    {
        character_left.sprite = imageDic["Empty"];
        character_right.sprite = imageDic["Empty"];
        ReadText(_asset);
        ShowDialogRow();
    }
    public void ReadText(UnityEngine.TextAsset _asset)
    {
        dialogRows = _asset.text.Split('\n');
        foreach (var row in dialogRows)
        {
            string[] cell = row.Split(',');
        }
    }

    public void ShowDialogRow()
    {
        for(int i = 0; i < dialogRows.Length; i++) 
        {
            string[] cells = dialogRows[i].Split(",");
            if (cells[0] == "!" && int.Parse(cells[1]) == targetDialogID)
            {
                dialogSystem.SetActive(false);
                caseBoard.SetActive(true);
                UpdateStory(cells[4]);

                targetDialogID = int.Parse(cells[5]);
                storyNextButton.SetActive(true);
                break;
            }
            else if (cells[0] == "$" && int.Parse(cells[1]) == targetDialogID)
            {
                dialogSystem.SetActive(true);
                caseBoard.SetActive(false);
                UpdateDialog(cells[2], cells[3], cells[4]);

                targetDialogID = int.Parse(cells[5]);
                dialogNextButton.SetActive(true);
                break;
            }
            else if(cells[0] == "#" && int.Parse(cells[1]) == targetDialogID)
            {
                storyNextButton.SetActive(false);
                GenerateSelectionButton(i);
            }
            else if (cells[0] == "%" && int.Parse(cells[1]) == targetDialogID)
            {
                dialogNextButton.SetActive(false);
                GenerateSelectionButton(i);
            }
        }
    }
    public void OnClickNext()
    {
        ShowDialogRow();
    }

    public void GenerateSelectionButton(int _index)
    {
        int intellegenceValue;
        int kindnessValue;
        int complianceValue;
        string[] cells = dialogRows[_index].Split(",");
        if (cells[0] == "#")
        {
            GameObject button = Instantiate(storySelectionButton, storyButtonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    if (cells[6] == "") kindnessValue = 0; else kindnessValue = int.Parse(cells[6]);
                    if (cells[7] == "") intellegenceValue = 0; else intellegenceValue = int.Parse(cells[7]);
                    if (cells[8] == "") complianceValue = 0; else complianceValue = int.Parse(cells[8]);
                    DialogSelectionClick(int.Parse(cells[5]), kindnessValue, intellegenceValue, complianceValue);
                }); 
            GenerateSelectionButton(_index + 1);
        }
        else if (cells[0] == "%")
        {
            GameObject button = Instantiate(dialogSelectionButton, dialogButtonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    if (cells[6] == "") kindnessValue = 0; else kindnessValue = int.Parse(cells[6]);
                    if (cells[7] == "") intellegenceValue = 0; else intellegenceValue = int.Parse(cells[7]);
                    if (cells[8] == "") complianceValue = 0; else complianceValue = int.Parse(cells[8]);
                    DialogSelectionClick(int.Parse(cells[5]), kindnessValue, intellegenceValue, complianceValue);
                });
            GenerateSelectionButton(_index + 1);
        }
    }

    public void StorySelectionClick(int _targrtIndex,int kindnessValue,int intellegenceValue,int complianceValue)
    {
        targetDialogID = _targrtIndex;
        TakeSelectionEffect(kindnessValue, intellegenceValue, complianceValue);
        ShowDialogRow();
        for(int i = 0;i < storyButtonGroup.childCount;i++)
        {
            Destroy(storyButtonGroup.GetChild(i).gameObject);
        }
    }
    public void DialogSelectionClick(int _targrtIndex, int kindnessValue, int intellegenceValue, int complianceValue)
    {
        targetDialogID = _targrtIndex;
        TakeSelectionEffect(kindnessValue, intellegenceValue, complianceValue);
        ShowDialogRow();
        for (int i = 0; i < dialogButtonGroup.childCount; i++)
        {
            Destroy(dialogButtonGroup.GetChild(i).gameObject);
        }
    }

    public void TakeSelectionEffect(int kindnessValue, int intellegenceValue, int complianceValue)
    {
        UIManager.instance.ChangeValue(kindnessValue, intellegenceValue, complianceValue);
    }
}
