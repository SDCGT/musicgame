using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMusicInfo
{
    private static float endTime;
    private static float prepeartime;//预备节拍时间

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
}
