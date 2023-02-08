using System.Collections.Generic;
using util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
