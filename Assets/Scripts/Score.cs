using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text loseScore;
    [SerializeField] Text bestScoreText;
    public int score;
    //int bestScore;
    //private void Start()
    //{
    //    if (YandexGame.SDKEnabled)
    //    {
    //        BestScore();
    //    }
    //}

    public void ScoreValue(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        if (score > YandexGame.savesData.bestScore)
        {
            YandexGame.savesData.bestScore = score;
            YandexGame.NewLeaderboardScores("TopPlayers", score);
            YandexGame.SaveProgress();
        }
        BestScore();
    }
    public void BestScore()
    {
        bestScoreText.text = YandexGame.savesData.bestScore.ToString();
        LoseScore();
    }
    public void LoseScore()
    {
        loseScore.text = score.ToString();
    }

}
