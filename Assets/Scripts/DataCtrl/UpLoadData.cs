using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.Networking;

public class UpLoadData : MonoBehaviour
{
    public static List<RecordData> scoreList = new List<RecordData>();
    public static List<RecordData> singList = new List<RecordData>();
    public static string recordString;
    private string url = "http://43.136.68.90:13763/insert?goal=";

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

    public void Start()
    {
        StartCoroutine(GetRequest(url));
    }
    public void UpLoad()
    {
        scoreList = PitchRecord.GetScoreList();
        singList = PitchRecord.GetSingList();
        var compareList = new CompareDataGroup();
        int a = scoreList.Count;
        int b = singList.Count;
        Debug.Log("1111111");
        int count = Mathf.Min(a, b);

        for(int i=0;i<count;i++)
        {
            CompareData data = new CompareData();
            data.singtime = singList[i].time;
            data.scoretime = scoreList[i].time;
            data.singFrequent = singList[i].Frequent;
            string singf = data.singFrequent.ToString("#0.0");
            data.scoreFrequent = scoreList[i].Frequent;
            string scoref = data.scoreFrequent.ToString("#0.0");
            compareList.compareList.Add(data);
            string recordForOneTime = "R" + scoref + "S" + singf;
            recordString = recordString + recordForOneTime;
        }
        string json = JsonUtility.ToJson(compareList);
        string filePath = Application.dataPath + "/Data/data.json";
        File.WriteAllText(filePath, recordString);//存储到本地json文件
        StartCoroutine(GetRequest(url+ recordString));
    }

    IEnumerator GetRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                //Debug.Log(json);
            }
         }
        yield return null;
    }
        
}
  

