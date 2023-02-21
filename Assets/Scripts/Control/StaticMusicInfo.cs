using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMusicInfo
{
    private static float endTime;
    private static float prepeartime;//预备节拍时间
    private static float singName;
    private static float scoreName;
    private static int scoring;
    private static string scoreID;

    public static float GetEndTime()
    {
        return endTime;
    }

    public static void SetEndTime(float time)
    {
        endTime = time;
    }

    public static float GetPrepearTime()
    {
        return prepeartime;
    }

    public static void SetPrepearTime(float time)
    {
       prepeartime = time;
    }

    public static float GetSingName()
    {
        return singName;
    }

    public static void SetSingName(float name)
    {
        singName = name;
    }

    public static float GetScoreName()
    {
        return scoreName;
    }

    public static void SetScoreName(float name)
    {
        scoreName = name;
    }

    public static int GetScoring()
    {
        return scoring;
    }

    public static void SetScoring(int _scoring)
    {
        scoring = _scoring;
    }

    public static string GetScoreID()
    {
        return scoreID;
    }

    public static void SetScoreID(string _scoreid)
    {
        scoreID = _scoreid;
    }
}
