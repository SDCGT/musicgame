using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    private GameObject _parentObject;
    private List<float> _screenSize;
    private CommonParams _commonParams = CommonParams.GetInstance();

    GameObject MainMenuBtn;
    GameObject RestartBtn;
    GameObject ExitBtn;
    void Start()
    {
        Timer.EndUI += InitiateEndMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitiateEndMenu()
    {
        Debug.Log("GameEnd");//������Ϸ
        MainMenuBtn = GameObject.Instantiate(_commonParams.GetMainMenuButton(),
            _parentObject.transform.position, _commonParams.GetMainMenuButton().transform.rotation);
        MainMenuBtn.transform.SetParent(_parentObject.transform);
        RectTransform mainRect = MainMenuBtn.GetComponent<RectTransform>();
        mainRect.position = new Vector3(_screenSize[0] / 2 + 300, _screenSize[1] - 540, 0);
        mainRect.sizeDelta = new Vector2(470, 414);

        Button mainButton = MainMenuBtn.GetComponent<Button>();
        mainButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScene");//�ص���ʼ�˵�
        });

        RestartBtn = GameObject.Instantiate(_commonParams.GetRestartButton(),
           _parentObject.transform.position, _commonParams.GetRestartButton().transform.rotation);
        RestartBtn.transform.SetParent(_parentObject.transform);
        RectTransform restartRect = RestartBtn.GetComponent<RectTransform>();
        restartRect.position = new Vector3(_screenSize[0] / 2 - 300, _screenSize[1] - 600, 0);
        restartRect.sizeDelta = new Vector2(529, 422);
        Button restartButton = RestartBtn.GetComponent<Button>();
        restartButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("LevelScene");//���¿�ʼ
            Time.timeScale = 1;
        });

        ExitBtn = GameObject.Instantiate(_commonParams.GetExitButton(),
          _parentObject.transform.position, _commonParams.GetExitButton().transform.rotation);
        ExitBtn.transform.SetParent(_parentObject.transform);
        RectTransform exitRect = ExitBtn.GetComponent<RectTransform>();
        exitRect.position = new Vector3(_screenSize[0] / 2 - 50, _screenSize[1] - 300, 0);
        exitRect.sizeDelta = new Vector2(353, 313);
        Button exitButton = ExitBtn.GetComponent<Button>();
        exitButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            Application.Quit();
        });
    }

    void OnDestroy()
    {
        // ȡ��ע���¼��������
        Debug.Log("ȡ��ע��");
        Timer.EndUI -= InitiateEndMenu;
    }
}
