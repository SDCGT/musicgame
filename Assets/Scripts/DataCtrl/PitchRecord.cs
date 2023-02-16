using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public static class PitchRecord
{
    public static Dictionary<float, double> _scoreDictionary = new Dictionary<float, double>();
    public static Dictionary<float, double> _singDictionary = new Dictionary<float, double>();


    public static Dictionary<float, double> GetSingDictionary()
    {
        return _singDictionary;
    }

    public static void SetSingDictionary(float time, double frequent)
    {
        _singDictionary.Add(time, frequent);
    }

    public static Dictionary<float, double> GetScoreDictionary()
    {
        return _scoreDictionary;
    }

    public static void SetScoreDictionary(float time,double frequent)
    {
        _scoreDictionary.Add(time, frequent);
    }

}
