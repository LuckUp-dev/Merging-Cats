using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Score score;
    [SerializeField] GameObject loseMenu;
    public void Restart()
    {
        gameController.isRestart = true;
        gameController.lose = false;        //�������� ����� �������� ��-�� ���� ��� ��������� ������ �����
        gameController.isRestart = false;
        loseMenu.SetActive(false);
        gameController.SpawnFirstCat();
        gameController.NextCatSpawn();
        score.score = 0;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
