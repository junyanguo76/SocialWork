using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    public GameObject storySystem;
    public GameObject dialogSystem;

    public int targetDialogID;
    public string[] dialogRows;

    public static DialogManager instance;

    public float typingSpeed = 0.05f;
    private Coroutine typingCoroutine;
    private Coroutine typingSound;

    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public int caseCount = 0;
    public TextAsset endFile;

    public AudioClip clickSound; // 点击时播放的音效
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        imageDic["Empty"] = sprites[0];
        imageDic["Me"] = sprites[1];
        imageDic["Mark"] = sprites[2];
        imageDic["Lily"] = sprites[3];
        imageDic["Therapist"] = sprites[4];
        imageDic["Vivian"] = sprites[5];
        imageDic["Vivian's Mother"] = sprites[6];
        imageDic["Vivian's Father"] = sprites[7];
        imageDic["Ryan"] = sprites[8];
        imageDic["Caroline"] = sprites[9];
        imageDic["Kim"] = sprites[10];
        imageDic["Kim's Father"] = sprites[11];
        imageDic["Kim's Mother"] = sprites[12];
        imageDic["Alex"] = sprites[13];
        imageDic["Alex's Father"] = sprites[14];

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clickSound;
    }

    private bool hasResetTargetDialog = false;
    private void Update()
    {
        if (caseCount == 3 && !hasResetTargetDialog)
        {
            targetDialogID = 0;
            hasResetTargetDialog = true; // 确保只执行一次
            ReadText(endFile);
            ShowDialogRow();
        }
    }

    public void UpdateStory(string _storyText)
    {
        if (_storyText != null)
        {
            storyText.text = _storyText;
        }
    }

    public void UpdateDialog(string _name, string _position, string _characterText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);  // 停止之前的打字动画
            StopCoroutine(typingSound); // 停止音效播放协程
            AudioSource.Stop(); // 停止音效
        }

        // 重置音效和打字状态
        if (_position == "l")
        {
            dialogBox_left.SetActive(true);
            dialogBox_right.SetActive(false);
            if (_name != null)
            {
                character_left.sprite = imageDic[_name];
                nameText_left.text = _name;
                dialogBox_left.GetComponentInChildren<TMP_Text>().text = _characterText;
            }
            
        }
        else if (_position == "r")
        {
            dialogBox_left.SetActive(false);
            dialogBox_right.SetActive(true);
            if (_name != null)
            {
                character_right.sprite = imageDic[_name];
                nameText_right.text = _name;
            }
            typingCoroutine = StartCoroutine(TypeSentence(dialogBox_right.GetComponentInChildren<TMP_Text>(), _characterText));
            typingSound = StartCoroutine(TypeSound());
        }
    }

    private IEnumerator TypeSentence(TMP_Text textComponent, string sentence)
    {
        textComponent.text = sentence; // 设置完整文本
        textComponent.ForceMeshUpdate(); // 强制文本更新

        textComponent.maxVisibleCharacters = 0; // 从0个字符开始
        yield return null; // 等待一帧，确保TMP正确计算换行

        int totalCharacters = textComponent.textInfo.characterCount; // 获取文本字符总数

        for (int i = 0; i < totalCharacters; i++)
        {
            textComponent.maxVisibleCharacters = i + 1; // 每次显示一个字符
            yield return new WaitForSeconds(typingSpeed); // 控制打字速度
        }

        // 打字动画结束，停止音效
        AudioSource.Stop();
    }

    private IEnumerator TypeSound()
    {
        AudioSource.loop = true; // 启用音效循环
        if (!AudioSource.isPlaying) // 如果音效没有在播放，则播放
        {
            AudioSource.Play(); // 播放音效并循环
        }
        yield return new WaitForSeconds(typingSpeed * 0.5f); // 音效的播放时长与打字速度同步
    }

    private IEnumerator StoryFirstShow()
    {
        dialogSystem.SetActive(false);
        yield return new WaitForSeconds(1);
        storySystem.SetActive(true);
        dialogSystem.SetActive(true);
    }

    public void StartAStory(TextAsset _asset)
    {
        targetDialogID = 0;
        ReadText(_asset);
        ShowDialogRow();
    }

    public void ReadText(TextAsset _asset)
    {
        dialogRows = _asset.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
    }

    public void ShowDialogRow()
    {
        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(",");

            // 确保数组长度足够，防止索引越界
            if (cells.Length <= 9)
                continue;

            // 解析 cells[1]，避免 FormatException
            if (!int.TryParse(cells[1].Trim(), out int dialogID))
                continue; // 如果解析失败，跳过本次循环

            if (dialogID == targetDialogID && (cells[9] == "City" || cells[9] == "House" 
                || cells[9] == "Office" || cells[9] == "Coffe" || cells[9] == "Meeting"))
            {
                character_left.sprite = imageDic["Empty"];
                character_right.sprite = imageDic["Empty"];
                nameText_left.text = null;
                nameText_right.text = null;
                GameManager.Instance.JumpToNewScene(cells[9]);

            }
            if (cells[10] == "key" && dialogID == targetDialogID)
            {
                audioSource.PlayOneShot(clickSound);
            }

            if (cells[0] == "End" && dialogID == targetDialogID)
            {
                dialogSystem.SetActive(false);
                storySystem.SetActive(false);
                caseCount++;
            }
            else if (cells[0] == "!" && dialogID == targetDialogID)
            {
                if(cells[9].Length >=1)
                {
                    StartCoroutine(StoryFirstShow());
                }
                else
                {
                    dialogSystem.SetActive(false);
                    storySystem.SetActive(true);
                    dialogSystem.SetActive(true);
                }
                UpdateStory(cells[4]);

                if (int.TryParse(cells[5].Trim(), out int nextDialogID))
                    targetDialogID = nextDialogID;

                storyNextButton.SetActive(true);
                break;
            }
            else if (cells[0] == "$" && dialogID == targetDialogID)
            {
                dialogSystem.SetActive(true);
                storySystem.SetActive(false);
                UpdateDialog(cells[2], cells[3], cells[4]);

                if (int.TryParse(cells[5].Trim(), out int nextDialogID))
                    targetDialogID = nextDialogID;

                dialogNextButton.SetActive(true);
                break;
            }
            else if (cells[0] == "#" && dialogID == targetDialogID)
            {
                storyNextButton.SetActive(false);
                GenerateSelectionButton(i);
            }
            else if (cells[0] == "%" && dialogID == targetDialogID)
            {
                if(cells[2] == "Me")
                {
                    character_left.sprite = imageDic[cells[2]];
                    nameText_left.text = cells[2];
                }
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
        int kindnessValue, intelligenceValue, complianceValue;
        string[] cells = dialogRows[_index].Split(",");
        if (cells[0] == "#")
        {
            GameObject button = Instantiate(storySelectionButton, storyButtonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                kindnessValue = string.IsNullOrEmpty(cells[6]) ? 0 : int.Parse(cells[6]);
                intelligenceValue = string.IsNullOrEmpty(cells[7]) ? 0 : int.Parse(cells[7]);
                complianceValue = string.IsNullOrEmpty(cells[8]) ? 0 : int.Parse(cells[8]);
                StorySelectionClick(int.Parse(cells[5]), kindnessValue, intelligenceValue, complianceValue);
            });
            GenerateSelectionButton(_index + 1);
        }
        else if (cells[0] == "%")
        {
            GameObject button = Instantiate(dialogSelectionButton, dialogButtonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                kindnessValue = string.IsNullOrEmpty(cells[6]) ? 0 : int.Parse(cells[6]);
                intelligenceValue = string.IsNullOrEmpty(cells[7]) ? 0 : int.Parse(cells[7]);
                complianceValue = string.IsNullOrEmpty(cells[8]) ? 0 : int.Parse(cells[8]);
                DialogSelectionClick(int.Parse(cells[5]), kindnessValue, intelligenceValue, complianceValue);
            });
            GenerateSelectionButton(_index + 1);
        }
    }

    public void StorySelectionClick(int _targetIndex, int kindnessValue, int intelligenceValue, int complianceValue)
    {
        targetDialogID = _targetIndex;
        TakeSelectionEffect(kindnessValue, intelligenceValue, complianceValue);
        ShowDialogRow();
        for (int i = 0; i < storyButtonGroup.childCount; i++)
        {
            Destroy(storyButtonGroup.GetChild(i).gameObject);
        }
    }

    public void DialogSelectionClick(int _targetIndex, int kindnessValue, int intelligenceValue, int complianceValue)
    {
        targetDialogID = _targetIndex;
        TakeSelectionEffect(kindnessValue, intelligenceValue, complianceValue);
        ShowDialogRow();
        for (int i = 0; i < dialogButtonGroup.childCount; i++)
        {
            Destroy(dialogButtonGroup.GetChild(i).gameObject);
        }
    }

    public void TakeSelectionEffect(int kindnessValue, int intelligenceValue, int complianceValue)
    {
        UIManager.instance.ChangeValue(kindnessValue, intelligenceValue, complianceValue);
    }
}
