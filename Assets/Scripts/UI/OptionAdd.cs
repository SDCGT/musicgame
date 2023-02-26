using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class OptionAdd : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public float dropdownWidth;
    string path;
    void Start()
    {
        path = Application.streamingAssetsPath + "/MusicXml";
        RectTransform template = dropdown.transform.Find("Template") as RectTransform;

        // 设置下拉菜单的宽度
        template.sizeDelta = new Vector2(dropdownWidth, template.sizeDelta.y);

        // 重新计算下拉菜单的布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(template);

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        CollectAllScores();
        AddOption();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddOption()
    {
        for (int i = 0; i < StaticMusicInfo.GetScoreIDs().GetLength(0); i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(StaticMusicInfo.GetScoreIDs()[i]));
        }//往下拉菜单里添加元素
        
    }

    void CollectAllScores()
    {
        //string folderPath = Application.dataPath + "/Materials/MusicXml";

        // 获取文件夹内所有文件信息
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] fileInfo = directoryInfo.GetFiles("*.xml");

        // 初始化文件名数组
        string[] scoreIds = new string[fileInfo.Length];

        // 遍历所有文件信息并保存文件名
        for (int i = 0; i < fileInfo.Length; i++)
        {
            scoreIds[i] = fileInfo[i].Name;
        }
        scoreIds = scoreIds.Where(scoreIds => Path.GetExtension(scoreIds).ToLower() == ".xml").ToArray();
        StaticMusicInfo.SetScoreIDs(scoreIds);
    }

    private void OnDropdownValueChanged(int index)
    {
        string optionText = dropdown.options[index].text;
        StaticMusicInfo.SetScoreID(path +"/"+ optionText);
        StaticMusicInfo.SetUploadScoreID(optionText);
        //Debug.Log("Dropdown option " + optionText + " was clicked.");
        //Debug.Log(StaticMusicInfo.GetScoreID());
    }
}
