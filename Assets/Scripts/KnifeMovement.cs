using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    // Растояние на которое  премещаяется игрок влево/вправо 
    private float _lineWidth = 2f;


    private CharacterController _characterController;
    private Vector3 _forward = Vector3.zero;

    // Векторы для обработки имитации swipe
    private Vector2 startTouchPos = Vector2.zero;
    private Vector2 endTouchPos = Vector2.zero;

    // Текущая полоса, на которой находится игрок
    private Lane _lane = Lane.Mid;

    // Напрвление swipe`a
    private Direction _direction;

    private enum Lane
    {
        Left,
        Mid,
        Right
    }
    private enum Direction
    {
        Left,
        Right
    }



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // Получаем вектор движения вперед  и домножаем его на скорость и дельту времени
        _forward = transform.forward;
        _forward *= _speed * Time.deltaTime;


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
                _direction = Direction.Left;
                MoveLeft();
            }
            // Направление  свайпа вправо
            if (startTouchPos.x < endTouchPos.x)
            {
                _direction = Direction.Right;
                MoveRight();
            }
        }

        // Перемещаем игрока вперед каждый кадр
        _characterController.Move(_forward);
    }


    // Поворот на 90 градусов перед сменой направлени пути. И занимаем центральную полосу
    public void TurnLeft()
    {
        _lane = Lane.Mid;
        transform.Rotate(0f, -90f, 0f);
    }

    private void MoveLeft()
    {
        if (_lane != Lane.Left)
        {
            SwitchLine(_direction);
            Vector3 left = -transform.right;
            left *= _lineWidth;
            _characterController.Move(left);

        }
    }
    private void MoveRight()
    {
        if (_lane != Lane.Right)
        {
            SwitchLine(_direction);
            Vector3 right = transform.right;
            right *= _lineWidth;
            _characterController.Move(right);

        }
    }

    // Меняем полосу движения (если это возможно) 
    private void SwitchLine(Direction dir)
    {
        if (dir == Direction.Left)
        {
            switch (_lane)
            {
                case Lane.Left:
                    _lane = Lane.Left;
                    break;
                case Lane.Mid:
                    _lane = Lane.Left;
                    break;
                case Lane.Right:
                    _lane = Lane.Mid;
                    break;
                default:
                    break;
            }
        }

        if (dir == Direction.Right)
        {
            switch (_lane)
            {
                case Lane.Left:
                    _lane = Lane.Mid;
                    break;
                case Lane.Mid:
                    _lane = Lane.Right;
                    break;
                case Lane.Right:
                    _lane = Lane.Right;
                    break;
                default:
                    break;
            }
        }
    }

}