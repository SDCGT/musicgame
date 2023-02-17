using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class UpLoadData : MonoBehaviour
{
    public static List<RecordData> scoreList = new List<RecordData>();
    public static List<RecordData> singList = new List<RecordData>();

    [System.Serializable]
    public class CompareData
    {
        [SerializeField]
        public float singtime;
        [SerializeField]
        public double singFrequent;
        [SerializeField]
        public float scoretime;
        [SerializeField]
        public double scoreFrequent;
    }

    [SerializeField]
    public class CompareDataGroup
    {
        [SerializeReference]
        public List<CompareData> compareList = new List<CompareData>();
        public CompareDataGroup() => compareList.Add(new CompareData());
    }

    public void UpLoad()
    {
        scoreList = PitchRecord.GetScoreList();
        singList = PitchRecord.GetSingList();
        var compareList = new CompareDataGroup();
        int a = scoreList.Count;
        int b = singList.Count;
        //Debug.Log("a"+a+"b"+b);
        int count = Mathf.Min(a, b);

        for(int i=0;i<count;i++)
        {
            CompareData data = new CompareData();
            data.singtime = singList[i].time;
            data.scoretime = scoreList[i].time;
            data.singFrequent = singList[i].Frequent;
            data.scoreFrequent = scoreList[i].Frequent;
            compareList.compareList.Add(data);
        }
        string json = JsonUtility.ToJson(compareList);
        string filePath = Application.dataPath + "/Data/data.json";
        File.WriteAllText(filePath, json);//�洢������json�ļ�
        

    }

    IEnumerator UploadFile(string path, string uploadURL)
    {
        // ����һ���µ�UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(uploadURL, "POST");

        // ����һ���ϴ���formData
        WWWForm form = new WWWForm();

        // ����ϴ��ļ���formData
        form.AddBinaryData("file", File.ReadAllBytes(path), "file", "multipart/form-data");

        // ����formDataΪ�ϴ�����
        request.uploadHandler = new UploadHandlerRaw(form.data);

        // �����ϴ�����������
        request.SetRequestHeader("Content-Type", "multipart/form-data");

        // �������󲢵ȴ���Ӧ
        yield return request.SendWebRequest();

        // ����Ƿ��ϴ��ɹ�
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error uploading file: " + request.error);
        }
        else
        {
            Debug.Log("File upload successful!");
        }
    }
}
