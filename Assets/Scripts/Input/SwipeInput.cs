using UnityEngine;
using UnityEngine.Events;


public class SwipeInput : MonoBehaviour
{

    // Векторы для обработки имитации swipe
    private Vector2 startTouchPos = Vector2.zero;
    private Vector2 endTouchPos = Vector2.zero;


    public UnityEvent SwipeLeft;
    public UnityEvent SwipeRight;



    void Update()
    {
        // Фиксируем точку нажатия ЛКМ(пальца)
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
        }
        // Фиксируем точку отжатия ЛКМ(пальца) и в зависимости от направления свайпа перемещаем игрока
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPos = Input.mousePosition;

            // Направление  свайпа влево
            if (startTouchPos.x > endTouchPos.x)
            {
                SwipeLeft.Invoke();
            }

            // Направление  свайпа вправо
            if (startTouchPos.x < endTouchPos.x)
            {
                SwipeRight.Invoke();
            }
        }
    }
}