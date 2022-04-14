using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

namespace UCARPG.View.UI
{
    public class GameMenuUIController : MonoBehaviour, IMenu
    {
        public event Action Opened;
        public event Action Closed;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _exitButton;

        public void Open()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            _restartButton.Select();
            Opened?.Invoke();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            Closed?.Invoke();
        }

        private void Awake()
        {
            gameObject.SetActive(false);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnExitButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}