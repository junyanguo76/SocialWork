using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        int width = Screen.width;
        int height = Screen.height;

        // Ç¿ÖÆÊÊÅä 16:9
        if ((float)width / height > 16f / 9f)
        {
            width = (int)(height * (16f / 9f));
        }
        else
        {
            height = (int)(width / (16f / 9f));
        }

        Screen.SetResolution(width, height, FullScreenMode.Windowed);
    }
}
