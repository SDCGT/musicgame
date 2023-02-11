using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMusicInfo
{
    private static float endTime;

    public static float GetEndTime()
    {
        return endTime;
    }

    public static void SetEndTime(float time)
    {
        endTime = time;
    }
}
