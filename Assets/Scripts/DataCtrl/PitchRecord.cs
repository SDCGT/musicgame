using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class RecordData
{
    public float time;
    public double Frequent;

}
public static class PitchRecord
{
    //public static Dictionary<float, double> _scoreDictionary = new Dictionary<float, double>();
    //public static Dictionary<float, double> _singDictionary = new Dictionary<float, double>();
    public static List<RecordData> _scoreList = new List<RecordData>();
    public static List<RecordData> _singList= new List<RecordData>();

    /*public static Dictionary<float, double> GetSingDictionary()
    {
        return _singDictionary;
    }*/

    public static List<RecordData> GetSingList()
    {
        return _singList;
    }

    /*public static void SetSingDictionary(float time, double frequent)
    {
        _singDictionary.Add(time, frequent);
    }*/

    public static void SetSingList(RecordData data)
    {
        _singList.Add(data);
    }

    /*public static Dictionary<float, double> GetScoreDictionary()
    {
        return _scoreDictionary;
    }*/

    public static List<RecordData> GetScoreList()
    {
        return _scoreList;
    }

    /*public static void SetScoreDictionary(float time,double frequent)
    {
        _scoreDictionary.Add(time, frequent);
    }*/
    public static void SetScoreList(RecordData data)
    {
        _scoreList.Add(data);
    }

}
