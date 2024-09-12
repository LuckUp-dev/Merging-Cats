using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Advertising : MonoBehaviour
{
    GameController gameController;


    [SerializeField] private Button rewardButton;
    [SerializeField] GameObject catsToChose;

    [SerializeField] Button smallCat;
    [SerializeField] Button littleCat;
    [SerializeField] Button xLittleCat;
    [SerializeField] Button medium;


    public bool isAdvertising = false;      //”ничтожать если стоит тру

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

    }

    public void ShowAdvertising()
    {
        if (!gameController.lose)
        {
            gameController.MusicStop();
            gameController.needToChose = true;
            YandexGame.RewVideoShow(1);
            //RewardSetActive();
            isAdvertising = true;
        }
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += RewardSetActive;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardSetActive;
    }

    private void RewardSetActive(int id)
    {
        gameController.needToChose = true;

        catsToChose.SetActive(true);
    }

    public void SpawnSmallCat()
    {
        isAdvertising = false;
        gameController.SpawnRewardCat(0);
        StartCoroutine(NeedToChoseTimer());
        catsToChose.SetActive(false);
        gameController.MusicPlay();
        gameController.DelayCouratine();

    }
    public void SpawnLittleCat()
    {
        isAdvertising = false;
        gameController.SpawnRewardCat(1);
        StartCoroutine(NeedToChoseTimer());
        catsToChose.SetActive(false);
        gameController.MusicPlay();
        gameController.DelayCouratine();


    }
    public void SpawnxLittleCat()
    {
        isAdvertising = false;
        gameController.SpawnRewardCat(2);
        StartCoroutine(NeedToChoseTimer());
        catsToChose.SetActive(false);
        gameController.MusicPlay();
        gameController.DelayCouratine();


    }
    public void SpawnMediumCat()
    {
        isAdvertising = false;
        gameController.SpawnRewardCat(3);
        StartCoroutine(NeedToChoseTimer());
        catsToChose.SetActive(false);
        gameController.MusicPlay();
        gameController.DelayCouratine();

    }

    private IEnumerator NeedToChoseTimer()
    {
        yield return new WaitForSeconds(0.15f);
        gameController.needToChose = false;
    }


}

