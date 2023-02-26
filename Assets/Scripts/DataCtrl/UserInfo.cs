using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserInfo
{

    public static string userID;
    public static void SetID(string _id)
    {
        userID = _id;
    }

    public static string GetID()
    {
        return userID;
    }
}
