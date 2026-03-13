using ProjectZomboid.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectZomboid.UI
{
    public class GameOverUI : GenericMonoSingleton<GameOverUI>
    {
        [SerializeField] private GameObject gameOverPanel;

        protected override void Awake()
        {
            base.Awake();
            gameOverPanel.SetActive(false);
        }

        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            //Time.timeScale = 0f;
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restarting game...");
        }
    }
}