using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class GameState : MonoBehaviour
    {
        public static GameState Singleton;

        public TextMeshProUGUI RocketsLeftText;
        public TextMeshProUGUI gameOverText;
        public static float GravitationalConstant = 6.6743e-6f * 5;
        public int RocketLimit = 3;

        private int _rocketsLeft;
        private float _timeState = 0f;
        private bool _isRocketLaunched;
        private bool _isGameOver;


        private void Start()
        {
            Singleton = this;
            ResetStatus();
        }

        private void ResetStatus()
        {
            _rocketsLeft = RocketLimit;
            _timeState = 0f;
            _isRocketLaunched = false;
            _isGameOver = false;
            RocketsLeftText.text = $"Rockets Left: {_rocketsLeft}/{RocketLimit}";
            gameOverText.text = "";
        }

        private void Update()
        {
            if (_isGameOver)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    ResetStatus();
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isRocketLaunched)
            {

            }
            else
            {

                float y = 0;
                if (Input.GetButton("Forward"))
                {
                    y += 1;
                }
                if (Input.GetButton("Backward"))
                {
                    y -= 1;
                }
                _timeState += y;
            }
        }

        public static float GetTimeState()
        {
            return Singleton.GetTimeStateInternal();
        }

        private float GetTimeStateInternal()
        {
            return _timeState;
        }

        public static bool IsRocketLaunched()
        {
            return Singleton.IsRocketLaunchedInternal();
        }

        private bool IsRocketLaunchedInternal()
        {
            return _isRocketLaunched;
        }

        public static void LaunchRocket()
        {
            Singleton.LaunchRocketInternal();
        }

        private void LaunchRocketInternal()
        {
            _rocketsLeft--;
            _isRocketLaunched = true;

            RocketsLeftText.text = $"Rockets Left: {_rocketsLeft}/{RocketLimit}";
        }

        public static void ResetRocket()
        {
            Singleton.ResetRocketInternal();
        }

        private void ResetRocketInternal()
        {
            _isRocketLaunched = false;
            if (_rocketsLeft == 0)
            {
                GameOver(false);
            }
        }

        public static bool IsGameOver()
        {
            return Singleton.IsGameOverInternal();
        }

        private bool IsGameOverInternal()
        {
            return _isGameOver;
        }

        public static void GameOver(bool win)
        {
            Singleton.GameOverInternal(win);
        }

        private void GameOverInternal(bool win)
        {
            if (_isGameOver) 
            {
                return;
            }
            _isGameOver = true;
            if (win)
            {
                gameOverText.text = "Way to Go,\nSpace Dandy!";
            }
            else
            {
                gameOverText.text = "See You\nSpace Cowboy...";
            }
            gameOverText.text += "\nPress Esc to Restart";
        }
    }
}