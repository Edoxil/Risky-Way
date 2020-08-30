using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    // Растояние на которое  премещаяется игрок влево/вправо 
    private float _laneWidth = 2f;


    private CharacterController _characterController;
    private Vector3 _forward = Vector3.zero;



    // Текущая полоса, на которой находится игрок
    public Lane lane = Lane.Mid;

    private ForwardAxis _forwardAxis = ForwardAxis.X;

    public enum Lane
    {
        Left,
        Mid,
        Right
    }
    private enum ForwardAxis
    {
        X,
        Z
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

        // Перемещаем игрока вперед каждый кадр
        _characterController.Move(_forward);
    }



    // Поворот на 90 градусов перед сменой направлени пути. И занимаем центральную полосу
    public void TurnLeft()
    {
        _forwardAxis = ForwardAxis.Z;
        AlignPosition();

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= 90f;
        transform.DORotate(rot, 0.5f);
        transform.DORestart();
        DOTween.Play(transform);
    }

    public void MoveLeft()
    {
        if (lane != Lane.Left)
        {
            Vector3 pos = transform.position;
            if (_forwardAxis == ForwardAxis.X)
            {
                SwitchLaneLeft();
                pos.x -= _laneWidth;
                transform.DOMoveX(pos.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            else
            {
                SwitchLaneLeft();
                pos.z -= _laneWidth;
                transform.DOMoveZ(pos.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
        }
    }
    public void MoveRight()
    {
        if (lane != Lane.Right)
        {
            Vector3 right = transform.position;
            if (_forwardAxis == ForwardAxis.X)
            {
                SwitchLaneRight();
                right.x += _laneWidth;
                transform.DOMoveX(right.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();


            }
            else
            {
                SwitchLaneRight();
                right.z += _laneWidth;
                transform.DOMoveZ(right.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
        }
    }

    private void AlignPosition()
    {
        Vector3 pos = transform.position;

        if (lane == Lane.Mid)
        {
            pos.z = 112f;
            transform.DOMoveZ(pos.z, 0.2f);
            transform.DORestart();
            transform.DOPlay();
        }
        if (lane == Lane.Left)
        {
            pos.z = 110f;
            transform.DOMoveZ(pos.z, 0.2f);
            transform.DORestart();
            transform.DOPlay();
        }
        if (lane == Lane.Right)
        {
            pos.z = 114f;
            transform.DOMoveZ(pos.z, 0.2f);
            transform.DORestart();
            transform.DOPlay();
        }
    }


    // Меняем полосу движения (если это возможно) 
    private void SwitchLaneLeft()
    {

        switch (lane)
        {
            case Lane.Left:
                lane = Lane.Left;
                break;
            case Lane.Mid:
                lane = Lane.Left;
                break;
            case Lane.Right:
                lane = Lane.Mid;
                break;
            default:
                break;
        }
    }
    private void SwitchLaneRight()
    {
        switch (lane)
        {
            case Lane.Left:
                lane = Lane.Mid;
                break;
            case Lane.Mid:
                lane = Lane.Right;
                break;
            case Lane.Right:
                lane = Lane.Right;
                break;
            default:
                break;
        }
    }

}
