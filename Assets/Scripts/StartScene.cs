using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject StartPanel;
    public List<Image> images = new List<Image> ();
    Color tempColor;
    int temp = 0;
    public GameObject buttonA;
    public GameObject buttonB;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Image image in images)
        {
            tempColor = image.color;
            tempColor.a = 1;
        }
    }

    public void StartButton()
    {
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if(temp == 1)
        {
            foreach (Image image in images)
            {
                tempColor.a -= 0.15f *  Time.deltaTime;
                image.color = tempColor;
                if(tempColor.a <= 0) StartPanel.SetActive(false);
            }
        }

    }
    // Update is called once per frame

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }

    IEnumerator StartTimer()
    {
        
        temp = 1;
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        yield return new WaitForSeconds(1);
    }
}
