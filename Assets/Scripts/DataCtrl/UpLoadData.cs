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
        File.WriteAllText(filePath, json);//存储到本地json文件
        

    }

    IEnumerator UploadFile(string path, string uploadURL)
    {
        // 创建一个新的UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(uploadURL, "POST");

        // 创建一个上传的formData
        WWWForm form = new WWWForm();

        // 添加上传文件到formData
        form.AddBinaryData("file", File.ReadAllBytes(path), "file", "multipart/form-data");

        // 设置formData为上传数据
        request.uploadHandler = new UploadHandlerRaw(form.data);

        // 设置上传的数据类型
        request.SetRequestHeader("Content-Type", "multipart/form-data");

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 检查是否上传成功
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
