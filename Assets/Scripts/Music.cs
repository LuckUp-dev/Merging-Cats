using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Music : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] GameObject soundOffPictures;


    public void SoundOn()
    {
        if (gameController.soundEnabled)
        {
            YandexGame.savesData.musicOn = false;
            gameController.soundEnabled = YandexGame.savesData.musicOn;
            gameController.MusicStop();
            soundOffPictures.SetActive(true);
            YandexGame.SaveProgress();
        }
        else
        {
            YandexGame.savesData.musicOn = true;
            gameController.soundEnabled = YandexGame.savesData.musicOn;
            gameController.MusicPlay();
            soundOffPictures.SetActive(false);
            YandexGame.SaveProgress();
        }
    }

    public void PumpSound()
    {
        if (gameController.pumpEnabled) 
        {
            YandexGame.savesData.soundOn = false;
            gameController.pumpEnabled = YandexGame.savesData.soundOn;
            soundOffPictures.SetActive(true);
            YandexGame.SaveProgress();
        }
        else
        {
            YandexGame.savesData.soundOn = true;
            gameController.pumpEnabled = YandexGame.savesData.soundOn;
            soundOffPictures.SetActive(false);
            YandexGame.SaveProgress();
        }
    }
}

