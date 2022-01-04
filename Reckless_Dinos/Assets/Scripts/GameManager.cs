using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text currentScoreText = null;
    [SerializeField] Text currentHealthText = null;
    [SerializeField] Text noKnivesText = null;
    [SerializeField] PlayerMovement _pMove;
    [SerializeField] Text highestScoreText = null;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameWinnerPanel;

    public int _highestScore;
    public int _currentScore = 0;
    public int _currentHealth = 3;
    public int _noKnives = 0; 
    public bool _gamePaused = false;
    public bool _gameOver = false;

     

    //Process Singleton Pattern. 
    public static GameManager _singletonVar;
 

    // Start is called before the first frame update
    void Start()
    {
        _singletonVar = this;

        _currentHealth = 3;
        _currentScore = 0;
        _highestScore = PlayerPrefs.GetInt("HighestScore", _highestScore);
        //TakeDamage(1);
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = _currentScore.ToString();
        currentHealthText.text = _currentHealth.ToString();

        if(_currentHealth <= 0)
        {
            _pMove.FreezePosition();
            _pMove.SetDeadAnimation();
            _currentHealth = 0;
            _gameOver = true;

        }


        if(_gameOver)
        {
            ShowGameOverPanel();
        }

    }

    private void ShowGameOverPanel()
    {
        StartCoroutine(showPanelAfterDelay(_gameOverPanel));
    }

   /* public void ShowGameWinnerPanel()
    {
        StartCoroutine(showPanelAfterDelay(_gameWinnerPanel));
    }*/

    void SetHighestScore()
    {
        if(_currentScore > _highestScore)
        {
            _highestScore = _currentScore;
            currentScoreText.text = _currentScore.ToString();

            PlayerPrefs.SetInt("HighestScore", _highestScore);
        }
    }

    public void IncrementScore(int gift)
    {
        _currentScore += gift;
    }

    public void TakeDamage(int dmg)
    {
        _currentHealth -= dmg; 
    }

    public void IncrementHealth(int gift)
    {
        _currentHealth += gift; 
    }

    //DelayingCoroutine.
    IEnumerator showPanelAfterDelay(GameObject panel)
    {
        yield return new WaitForSeconds(0.85f);
        panel.SetActive(true);
        Debug.Log("Panel Appeared!");
    }
}
