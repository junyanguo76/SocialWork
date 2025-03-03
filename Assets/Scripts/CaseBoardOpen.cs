using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseBoardOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject caseBoard;
    public GameObject caseItself;
    public GameObject blockPic;
    public void openCase()
    {
        caseBoard.SetActive(true);
        blockPic.SetActive(true);

    }
    public void CloseCase()
    {
        blockPic.SetActive(false);
        caseItself.SetActive(false);
    }
}
