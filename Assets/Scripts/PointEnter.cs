using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button; // Присвойте кнопку в инспекторе
    private Vector3 originalScale;
    private float hoverScale = 1.1f;
    private float duration = 0.2f;
    private Coroutine scaleCoroutine;
    GameController gameController;
    public bool mouseOnButton = false;      //Murge курсор на рекламе, чтоб кот не падал при нажатии



    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (button == null)
        {
            button = GetComponent<Button>();
        }

        // Сохраняем изначальный размер кнопки
        originalScale = button.transform.localScale;
    }

    // Вызывается при наведении курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOnButton = true;

        gameController.mouseOnAdvertising = true;
        // Прерываем предыдущую корутину, если она выполняется
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // Запускаем корутину для увеличения размера
        scaleCoroutine = StartCoroutine(ScaleButton(originalScale * hoverScale));
    }

    // Вызывается при уходе курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOnButton = false;
        gameController.mouseOnAdvertising = false;

        // Прерываем предыдущую корутину, если она выполняется
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // Запускаем корутину для уменьшения размера
        scaleCoroutine = StartCoroutine(ScaleButton(originalScale));
    }

    // Корутина для плавного изменения размера
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
