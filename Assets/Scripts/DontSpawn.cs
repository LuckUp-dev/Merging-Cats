using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DontSpawn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameController gameController;
    public bool mouseOnButton = false;      //Murge ������ �� �������, ���� ��� �� ����� ��� �������

    // ���������� ��� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameController.mouseOnButton = true;

        gameController.mouseOnAdvertising = true;
    }

    // ���������� ��� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        gameController.mouseOnButton = false;

        gameController.mouseOnAdvertising = false;
    }
}
