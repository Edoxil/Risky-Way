using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    // Растояние на которое  премещаяется игрок влево/вправо 
    private float _laneWidth = 2f;

    private CharacterController _characterController;
    private Vector3 _forward = Vector3.zero;


    public bool isStoped = false;

    public UnityEvent Turning;

    // Текущая полоса, на которой находится игрок
    public Lane lane = Lane.Mid;

    // Ось по которой происходит движение вперед
    private DirectionAxis _forwardAxis = DirectionAxis.X;

    public enum Lane
    {
        Left,
        Mid,
        Right
    }
    private enum DirectionAxis
    {
        X, Z
    }




    private void Start()
    {
        Debug.Log("Не забыть поменять значения в AlignPosition когда будет получена информация о текущем уровне!!!");
        _characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (isStoped) { return; }
        // Получаем вектор движения вперед  и домножаем его на скорость и дельту времени
        if (_forwardAxis == DirectionAxis.X)
        {
            _forward = Vector3.forward;
            _forward *= _speed * Time.deltaTime;
        }
        else if (_forwardAxis == DirectionAxis.Z)
        {
            _forward = Vector3.left;
            _forward *= _speed * Time.deltaTime;
        }

        // Перемещаем игрока вперед каждый кадр
        _characterController.Move(_forward);
    }




    // Поворот на 90 градусов и меняем ось движени вперед
    public void TurnLeft()
    {

        Turning?.Invoke();
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= 90f;
        transform.DORotate(rot, 0.5f);
        transform.DORestart();
        DOTween.Play(transform);

        _forwardAxis = DirectionAxis.Z;

        AlignPosition();
    }
    public void SetDefoultPosition()
    {
        Vector3 pos = new Vector3(0f, 1.5f, 0f);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        transform.SetPositionAndRotation(pos, rot);
    }

    public void StartMoving()
    {
        isStoped = false;
    }




    // Движени влево/вправо в зависимости от оси движения вперед
    public void MoveLeft()
    {
        if (lane != Lane.Left)
        {
            Vector3 pos = transform.position;


            if (_forwardAxis == DirectionAxis.X)
            {

                SwitchLaneLeft();
                pos.x -= _laneWidth;
                transform.DOMoveX(pos.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            else if (_forwardAxis == DirectionAxis.Z)
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

            if (_forwardAxis == DirectionAxis.X)
            {

                SwitchLaneRight();
                right.x += _laneWidth;
                transform.DOMoveX(right.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            else if (_forwardAxis == DirectionAxis.Z)
            {
                SwitchLaneRight();
                right.z += _laneWidth;
                transform.DOMoveZ(right.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }

        }

    }


    #region EXPEREMENTAL
    private void AlignPosition()
    {
        Vector3 pos = transform.position;


        if (_forwardAxis == DirectionAxis.Z)
        {
            if (lane == Lane.Mid)
            {

                pos.z = 103f;
                transform.DOMoveZ(pos.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            if (lane == Lane.Left)
            {
                pos.z = 101f;
                transform.DOMoveZ(pos.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            if (lane == Lane.Right)
            {
                pos.z = 105f;
                transform.DOMoveZ(pos.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }





        }

    }
    #endregion EXPEREMENTAL

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
