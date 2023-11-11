﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class UI : MonoBehaviour
    {
        public static UI Singleton;

        public TextMeshProUGUI PetsReceivedText;
        public TextMeshProUGUI TimeLeftText;
        public TextMeshProUGUI gameOverText;
        public int RocketLimit = 3;

        private int _rocketsLeft;
        private float _timeState = 0f;
        private bool _isRocketFired;
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
            _isRocketFired = false;
            _isGameOver = false;
            //PetsReceivedText.text = $"Pets Received: {_petsReceived}/{_numHumans}";
            //TimeLeftText.text = $"Time Left: {_timeLeft}";
            //gameOverText.text = "";
        }

        private void Update()
        {
            if (_isRocketFired)
            {

            }
            else
            {
                float y = Input.GetAxis("Vertical");
                _timeState += y;
            }
            if (_isGameOver)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    ResetStatus();
                }
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

        public static bool IsRocketFired()
        {
            return Singleton.IsRocketFiredInternal();
        }

        private bool IsRocketFiredInternal()
        {
            return _isRocketFired;
        }

        public static void FireRocket()
        {
            Singleton.FireRocketInternal();
        }

        private void FireRocketInternal()
        {
            _rocketsLeft--;
            _isRocketFired = true;

            //PetsReceivedText.text = $"Pets Received: {_petsReceived}/{_numHumans}";

            if (_rocketsLeft == 0) // Todo: do this only when final rocket crashes
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
                gameOverText.text = "Way to Go, Space Dandy!";
            }
            else
            {
                gameOverText.text = "See You Space Cowboy...";
            }
            gameOverText.text += "\nPress Esc to Restart";
        }
    }
}