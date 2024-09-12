using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button; // ��������� ������ � ����������
    private Vector3 originalScale;
    private float hoverScale = 1.1f;
    private float duration = 0.2f;
    private Coroutine scaleCoroutine;
    GameController gameController;
    public bool mouseOnButton = false;      //Murge ������ �� �������, ���� ��� �� ����� ��� �������



    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (button == null)
        {
            button = GetComponent<Button>();
        }

        // ��������� ����������� ������ ������
        originalScale = button.transform.localScale;
    }

    // ���������� ��� ��������� �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOnButton = true;

        gameController.mouseOnAdvertising = true;
        // ��������� ���������� ��������, ���� ��� �����������
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // ��������� �������� ��� ���������� �������
        scaleCoroutine = StartCoroutine(ScaleButton(originalScale * hoverScale));
    }

    // ���������� ��� ����� �������
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOnButton = false;
        gameController.mouseOnAdvertising = false;

        // ��������� ���������� ��������, ���� ��� �����������
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // ��������� �������� ��� ���������� �������
        scaleCoroutine = StartCoroutine(ScaleButton(originalScale));
    }

    // �������� ��� �������� ��������� �������
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startScale = button.transform.localScale;

        while (elapsedTime < duration)
        {
            button.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.transform.localScale = targetScale;
        scaleCoroutine = null;
    }
}
