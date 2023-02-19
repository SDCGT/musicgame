using System.Collections.Generic;
using util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChangeScenes:MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text warning;
    // Start is called before the first frame update
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LogIn()
    {
        if(input.text != ""&&input.text!=null)
        {
            UserInfo.SetID(input.text);
            LoadStartScene();
        }

        if(input.text == ""||input.text==null)
        {
            warning.color = Color.red;
            warning.text = "请输入正确的ID！";
        }
    }
}
