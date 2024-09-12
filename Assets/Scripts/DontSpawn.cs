using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DontSpawn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameController gameController;
    public bool mouseOnButton = false;      //Murge курсор на рекламе, чтоб кот не падал при нажатии

    // Вызывается при наведении курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameController.mouseOnButton = true;

        gameController.mouseOnAdvertising = true;
    }

    // Вызывается при уходе курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        gameController.mouseOnButton = false;

        gameController.mouseOnAdvertising = false;
    }
}
