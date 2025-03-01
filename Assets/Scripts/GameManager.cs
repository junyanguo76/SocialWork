using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject CityPanel;
    public GameObject HousePanel;




    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }


    public void JumpToCase()
    {
        StartCoroutine(WaitForOneSecond());
        ScreenTransition.instance.StartBlackScreenTransition();

    }

    private IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(1f);
        CityPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        HousePanel.SetActive(true);
    }
}
