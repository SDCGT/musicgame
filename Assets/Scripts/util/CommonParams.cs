using UnityEngine;

namespace util
{
    public class CommonParams
    {
        private static CommonParams instance = new CommonParams();

        // 作为公共数据存储类
        private string _scoreName;
        private string _xmlFolderPath = "Assets/Materials/MusicXml";
        private GameObject _prefabSymbol;
        private GameObject _prefabText;
        private GameObject _prefabLine;
        private GameObject _prefabFileButton;
        private GameObject _ExitBtn;
        private GameObject _RestartBtn;
        private GameObject _MainMenuBtn;
        private GameObject _prefabDot;

        public static CommonParams GetInstance() { return instance; }

        public string GetScoreName()
        {
            if (Application.platform == RuntimePlatform.Android) // android
            {
                _scoreName += Application.streamingAssetsPath;
            }
            return _scoreName;
        }

        public void SetScoreName(string scoreName) { _scoreName = scoreName; }

        public string GetXmlFolderPath() { return _xmlFolderPath; }

        public void SetXmlFolderPath(string xmlFolderPath) { _xmlFolderPath = xmlFolderPath; }

        public GameObject GetPrefabSymbol() { return _prefabSymbol; }

        public void SetPrefabSymbol(GameObject prefabSymbol) { _prefabSymbol = prefabSymbol; }

        public GameObject GetPrefabLine() { return _prefabLine; }

        public void SetPrefabLine(GameObject prefabLine) { _prefabLine = prefabLine; }

        public GameObject GetPrefabDot() { return _prefabDot; }

        public void SetPrefabDot(GameObject prefabDot) { _prefabDot = prefabDot; }

        public GameObject GetPrefabText() { return _prefabText; }

        public void SetPrefabText(GameObject prefabText) { _prefabText = prefabText; }

        public GameObject GetPrefabFileButton() { return _prefabFileButton; }

        public void SetPrefabFileButton(GameObject prefabFileButton) { _prefabFileButton = prefabFileButton; }

        public GameObject GetExitButton() { return _ExitBtn; }

        public void SetExitButton(GameObject exitBtn) { _ExitBtn = exitBtn; }

        public GameObject GetMainMenuButton() { return _MainMenuBtn; }

        public void SetMainMenuButton(GameObject mainBtn) { _MainMenuBtn = mainBtn; }

        public GameObject GetRestartButton() { return _RestartBtn; }

        public void SetRestartButton(GameObject restartBtn) { _RestartBtn = restartBtn; }
    }
}