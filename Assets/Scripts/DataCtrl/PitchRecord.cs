using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public static class PitchRecord
{
    private static List<double> _singPitchList = new List<double>();
    private static List<double> _scorePitchList = new List<double>();

    public static List<double> GetSingPitchList()
    {
        return _singPitchList; 
    }

    public static void SetSingPitchList(List<double> singPitchList)
    {
        _singPitchList = singPitchList;
    }

    public static List<double> GetScorePitchList()
    {
        return _singPitchList;
    }

    public static void SetScorePitchList(List<double> singPitchList)
    {
        _singPitchList = singPitchList;
    }
}
