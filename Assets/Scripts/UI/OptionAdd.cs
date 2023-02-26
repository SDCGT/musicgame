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

        // ���������˵��Ŀ��
        template.sizeDelta = new Vector2(dropdownWidth, template.sizeDelta.y);

        // ���¼��������˵��Ĳ���
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
        }//�������˵������Ԫ��
        
    }

    void CollectAllScores()
    {
        //string folderPath = Application.dataPath + "/Materials/MusicXml";

        // ��ȡ�ļ����������ļ���Ϣ
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] fileInfo = directoryInfo.GetFiles("*.xml");

        // ��ʼ���ļ�������
        string[] scoreIds = new string[fileInfo.Length];

        // ���������ļ���Ϣ�������ļ���
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
