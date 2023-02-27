using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.Networking;
using TMPro;
using util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpLoadData : MonoBehaviour
{
    public static List<RecordData> scoreList = new List<RecordData>();
    public static List<RecordData> singList = new List<RecordData>();
    public static string recordString;
    public static int score;
    public static int starCount;
    public TMP_Text scoreText;
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
        //StartCoroutine(GetUnityRequest(url));
        recordString = "ID:"+UserInfo.GetID()+"ScoreID:"+StaticMusicInfo.GetUploadScoreID();
        GameObject canvus= GameObject.Find("Score");
        scoreText = canvus.GetComponent<TMP_Text>();
    }
    public void UpLoad()
    {
        scoreList = PitchRecord.GetScoreList();
        singList = PitchRecord.GetSingList();
        var compareList = new CompareDataGroup();
        int a = scoreList.Count;
        int b = singList.Count;
        //Debug.Log("1111111");
        int count = Mathf.Min(a, b);
        string scorestring = scoreText.text;
        int.TryParse(scorestring, out score);
        float scorePercent = score * 1.0f / (starCount * 100.0f);//��������
        Debug.Log("score" + scorePercent);//������
        string scorePercentStr = scorePercent.ToString("#0.00");
        recordString = recordString + "Score:" + scorePercentStr+"..";

        for (int i=0;i<count;i++)
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
            recordString = recordString +"List"+ recordForOneTime;
        }

        string filePath = Application.dataPath + "/Data/data.json";
        File.WriteAllText(filePath, recordString);//�洢������json�ļ�
        //StartCoroutine(GetRequest(url + recordString));//�����ı����ƶ�
        //StartCoroutine(GetUnityRequest(url + recordString));//�����ı����ƶ�
                                                            //string json = JsonUtility.ToJson(compareList);//

        WebClient client = new WebClient();
        client.DownloadStringAsync(new System.Uri(url+recordString+"end"));
        client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadCompleted);

    }

   /* IEnumerator GetRequest(string url)
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
    }*/

    private IEnumerator GetUnityRequest(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
        yield return www.SendWebRequest();
    }



    void DownloadCompleted(object sender, DownloadStringCompletedEventArgs args)
    {
        if (args.Error != null)
        {
            Debug.Log(args.Error.Message);
        }
        else
        {
            Debug.Log(args.Result);
        }
    }

    
}
  

