using System.Collections.Generic;
using util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace symbol
{
    public class ScoreView
    {
        private List<List<Measure>> _scoreList;
        private GameObject _parentObject;
        private List<float> _screenSize;
        private List<string> _scoreInfo;
        private ParamsGetter _paramsGetter = ParamsGetter.GetInstance();
        private CommonParams _commonParams = CommonParams.GetInstance();

        GameObject MainMenuBtn;
        GameObject RestartBtn;
        GameObject ExitBtn;

        public ScoreView(List<List<Measure>> scoreList, GameObject parentObject, List<float> screenSize, List<string> scoreInfo)
        {
            _parentObject = parentObject;
            _scoreList = scoreList;
            _screenSize = screenSize;
            _scoreInfo = scoreInfo;
            Init();
        }

        private void Init()
        {
            OnDraw();
        }

        private void OnDraw()
        {
            // 放置按钮
            PlaceButton();
            // 绘制乐谱信息
            DrawScoreInfo();

            // 绘制乐谱内容
            float startX = 67f / 2;
            float startY = _screenSize[1] - 250;
            Vector3 paragraphPosition = new Vector3(startX, startY, 0);
            // 遍历scoreList，对每一行来绘制
            for (int i = 0; i < _scoreList.Count; i++)
            {
                // 新建paragraph画布，每一行有自己的画布
                string objName = "Paragraph" + (i + 1);
                GameObject paragraphObject = new GameObject(objName);
                Canvas paragraphCanvas = paragraphObject.AddComponent<Canvas>();
                paragraphCanvas.transform.SetParent(_parentObject.transform);
                RectTransform rect = paragraphCanvas.GetComponent<RectTransform>();
                // 设置位置为以画布左下角为坐标原点
                rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.zero; rect.pivot = new Vector2(0.5f, 0.5f);
                rect.position = new Vector3(paragraphPosition.x,
                    paragraphPosition.y - 2 * _paramsGetter.GetTotalHeight() * i,
                    paragraphPosition.z);

                // 将paragraph画布对象赋为下一层的父对象
                // 绘制每一行的视图
                ParagraphView paragraphView = new ParagraphView(_scoreList[i], paragraphObject);
            }
        }

        // 绘制乐谱信息
        private void DrawScoreInfo()
        {
            Vector2 worktitlePosition = new Vector2(_screenSize[0] / 2, _screenSize[1] - 50);
            Vector2 creatorPosition = new Vector2(_screenSize[0] - 50, _screenSize[1] - 75);

            DrawText(_scoreInfo[0], worktitlePosition, 30);
            DrawText(_scoreInfo[1], creatorPosition, 10);
        }

        private void DrawText(string text, Vector2 position, int fontSize)
        {
            GameObject textObject = GameObject.Instantiate(_commonParams.GetPrefabText(),
                _parentObject.transform.position,
                _commonParams.GetPrefabText().transform.rotation);
            textObject.transform.SetParent(_parentObject.transform);
            RectTransform rect = textObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(500, 100);
            rect.position = new Vector3(position.x, position.y, 0);
            Text objectText = textObject.GetComponent<Text>();
            objectText.fontSize = fontSize;
            objectText.text = text;
        }

        // 放置button按钮暂停游戏
        private void PlaceButton()
        {
            // 返回按钮
            GameObject backButtonObject = GameObject.Instantiate(_commonParams.GetPrefabFileButton(),
                _parentObject.transform.position, _commonParams.GetPrefabFileButton().transform.rotation);
            backButtonObject.transform.SetParent(_parentObject.transform);
            RectTransform backRect = backButtonObject.GetComponent<RectTransform>();
            backRect.position = new Vector3(50, _screenSize[1] - 50, 0);
            backRect.sizeDelta = new Vector2(65, 65);
            Text backText = backButtonObject.GetComponentInChildren<Text>();
            backText.text = "";
            Button backButton = backButtonObject.GetComponent<Button>();
            backButton.onClick.AddListener(delegate
            {
                //SceneManager.LoadScene("StartScene");
                //if(Time.timeScale==1)
                //{
                    //Debug.Log("暂停");
                    Time.timeScale = 0;
                    InitiatePauseMenu();
                //}

                //SceneManager.LoadScene("StartScene");
               /*if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    Destroy(MainMenuBtn);
                    Destroy(RestartBtn);
                    Destroy(ExitBtn);
                }*/

            });

            /*
            GameObject exitButtonObject = GameObject.Instantiate(_commonParams.GetPrefabFileButton(),
                _parentObject.transform.position, _commonParams.GetPrefabFileButton().transform.rotation);
            exitButtonObject.transform.SetParent(_parentObject.transform);
            RectTransform exitRect = exitButtonObject.GetComponent<RectTransform>();
            exitRect.position = new Vector3(_screenSize[0] - 50, _screenSize[1] - 50, 0);
            exitRect.sizeDelta = new Vector2(50, 30);
            Text exitText = exitButtonObject.GetComponentInChildren<Text>();
            exitText.text = "Exit";
            Button exitButton = exitButtonObject.GetComponent<Button>();
            exitButton.onClick.AddListener(delegate
            {
                SystemCtrl.ResumeGame();
                //Application.Quit();
            });*/
        }

        private void InitiatePauseMenu()
        {
            MainMenuBtn= GameObject.Instantiate(_commonParams.GetMainMenuButton(),
                _parentObject.transform.position, _commonParams.GetMainMenuButton().transform.rotation);
            MainMenuBtn.transform.SetParent(_parentObject.transform);
            RectTransform mainRect = MainMenuBtn.GetComponent<RectTransform>();
            mainRect.position = new Vector3(_screenSize[0]/2+300, _screenSize[1] - 540, 0);
            mainRect.sizeDelta = new Vector2(470, 414);
            
            Button mainButton = MainMenuBtn.GetComponent<Button>();
            mainButton.onClick.AddListener(delegate
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("StartScene");//回到开始菜单
            });

            RestartBtn = GameObject.Instantiate(_commonParams.GetRestartButton(),
               _parentObject.transform.position, _commonParams.GetRestartButton().transform.rotation);
            RestartBtn.transform.SetParent(_parentObject.transform);
            RectTransform restartRect = RestartBtn.GetComponent<RectTransform>();
            restartRect.position = new Vector3(_screenSize[0] / 2-300, _screenSize[1] - 600, 0);
            restartRect.sizeDelta = new Vector2(529, 422);
            Button restartButton = RestartBtn.GetComponent<Button>();
            restartButton.onClick.AddListener(delegate
            {             
                SceneManager.LoadScene("LevelScene");//重新开始
                Time.timeScale = 1;
            });

            ExitBtn = GameObject.Instantiate(_commonParams.GetExitButton(),
              _parentObject.transform.position, _commonParams.GetExitButton().transform.rotation);
            ExitBtn.transform.SetParent(_parentObject.transform);
            RectTransform exitRect = ExitBtn.GetComponent<RectTransform>();
            exitRect.position = new Vector3(_screenSize[0] / 2-50, _screenSize[1] - 300, 0);
            exitRect.sizeDelta = new Vector2(353, 313);
            Button exitButton = ExitBtn.GetComponent<Button>();
            exitButton.onClick.AddListener(delegate
            {
                Debug.Log("GameEnd");//结束游戏
                Time.timeScale = 1;
                Application.Quit();
            });
        }
    }
}