using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCheck : MonoBehaviour
{
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        loseMenu.SetActive(true);
        gameController.lose = true;
    }
}

