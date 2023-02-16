using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CompearData
{
    public string scoretime;
    public string singFrequent;
    public string scoreFrequent;
}
public class UpLoadData : MonoBehaviour
{
    public void Start()
    {
        
    }
    public void UpLoad()
    {
        foreach (var item in PitchRecord.GetSingDictionary().Keys)
        {
            Debug.Log("singtime" + item);
        }
        foreach (var item in PitchRecord.GetScoreDictionary().Keys)
        {
            Debug.Log("scoretime" + item);
        }
        Debug.Log("UpLoad");
        int a = PitchRecord.GetSingDictionary().Count;
        int b = PitchRecord.GetScoreDictionary().Count;
        int count = Mathf.Min(a, b);
    }
}
