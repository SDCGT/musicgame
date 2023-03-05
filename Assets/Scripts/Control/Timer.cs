using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
    {
        public GameObject upLoadData1;
        public GameObject ResartBtn;
        public GameObject QuitBtn;
        public GameObject MainBtn;
        public GameObject _parentObject;
        public GameObject ResartBtn1;
        public GameObject QuitBtn1;
        public GameObject MainBtn1;

        float playTime;//视唱已持续时间,有偏差，增加初始值
        float endTime;//视唱结束时间
        bool start;
        bool end;
        void Start()
        {
            playTime = 0;
            endTime = 0;
            start = false;
            bool end = false;
            endTime = StaticMusicInfo.GetEndTime();
            PitchRecord.ClearData();
            //Debug.Log(PitchRecord.GetScoreList().Count);
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Debug.Log("playtime"+playTime);
            if (Input.GetKeyDown(KeyCode.Space))  //按下空格键后开始
            {
                start = true;
            }

            if (start && playTime < ((endTime)+StaticMusicInfo.GetPrepearTime()))//曲目时间内，计时器运作
            {
                playTime += Time.deltaTime;
                //Debug.Log("playtime" + playTime);
            }

            if (start && playTime >= ((endTime) + StaticMusicInfo.GetPrepearTime())&&!end)
            {
                upLoadData1.SendMessage("UpLoad");
                CallEndUI();
                Time.timeScale = 0;
                end = true;
            }

    }
        public float GetGameTime()
        {
            return playTime;
        }

        public bool GetStartBool()
        {
            return start;
        }

        public void CallEndUI()
        {
        MainBtn1 = GameObject.Instantiate(MainBtn,
           _parentObject.transform.position, MainBtn.transform.rotation);
        MainBtn1.transform.SetParent(_parentObject.transform);
        RectTransform mainRect = MainBtn1.GetComponent<RectTransform>();
        mainRect.position = new Vector3(1920 / 2 + 300, 1080- 540, 0);
        mainRect.sizeDelta = new Vector2(470, 414);

        Button mainButton = MainBtn1.GetComponent<Button>();
        mainButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScene");//回到开始菜单
        });

        ResartBtn1 = GameObject.Instantiate(ResartBtn,
           _parentObject.transform.position, ResartBtn.transform.rotation);
        ResartBtn1.transform.SetParent(_parentObject.transform);
        RectTransform restartRect = ResartBtn1.GetComponent<RectTransform>();
        restartRect.position = new Vector3(1920 / 2 - 300,1080 - 600, 0);
        restartRect.sizeDelta = new Vector2(529, 422);
        Button restartButton = ResartBtn1.GetComponent<Button>();
        restartButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("LevelScene");//重新开始
            Time.timeScale = 1;
        });

        QuitBtn1= GameObject.Instantiate(QuitBtn,
          _parentObject.transform.position, QuitBtn.transform.rotation);
        QuitBtn1.transform.SetParent(_parentObject.transform);
        RectTransform exitRect = QuitBtn1.GetComponent<RectTransform>();
        exitRect.position = new Vector3(1920/ 2 - 50, 1080 - 300, 0);
        exitRect.sizeDelta = new Vector2(353, 313);
        Button exitButton = QuitBtn1.GetComponent<Button>();
        exitButton.onClick.AddListener(delegate
        {
            Debug.Log("GameEnd");//结束游戏
            Time.timeScale = 1;
            Application.Quit();
        });
    }
}
        
    
