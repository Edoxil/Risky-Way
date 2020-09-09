using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    // Растояние на которое  премещаяется игрок влево/вправо 
    private float _laneWidth = 2f;
    [SerializeField] private NavMeshAgent _agent;
    private Vector3 _forward = Vector3.zero;


    public bool isStoped = false;

    [SerializeField] private GamePlayUI _gamePlayeUI = null;
    private Vector3 _finish = Vector3.zero;

    public UnityEvent Turning;


    // Текущая полоса, на которой находится игрок
    public Lane lane = Lane.Mid;
    // Ось по которой происходит движение вперед
    private DirectionAxis _directionAxis = DirectionAxis.X;

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







    private void Update()
    {
        if (isStoped) { return; }
        // Получаем вектор движения вперед  и домножаем его на скорость и дельту времени
        if (_directionAxis == DirectionAxis.X)
        {
            _forward = Vector3.forward;
            _forward *= _speed * Time.deltaTime;
        }
        else if (_directionAxis == DirectionAxis.Z)
        {
            _forward = Vector3.left;
            _forward *= _speed * Time.deltaTime;
        }

        // Перемещаем игрока вперед каждый кадр
        _agent.Move(_forward);
    }


    private void FixedUpdate()
    {
        if (isStoped) { return; }
        float distance = Vector3.Distance(transform.position, _finish);


        if (distance > 0)
        {
            _gamePlayeUI.SetProgressBarValue(distance);
        }

    }


    // Поворот на 90 градусов и меняем ось движени вперед
    public void TurnLeft()
    {

        Turning?.Invoke();
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= 90f;
        transform.DORotate(rot, 0.5f);
        transform.DORestart();
        transform.DOPlay();

        _directionAxis = DirectionAxis.Z;

        AlignPosition();
    }
    public void GameStartedHandler()
    {

        _agent.enabled = false;
        Vector3 pos = new Vector3(0f, 1.5f, 0f);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        transform.position = pos;
        transform.rotation = rot;
        _agent.enabled = true;
    }


    public void SetFinish(Vector3 finish)
    {
        _finish = finish;
    }






    // Движени влево/вправо в зависимости от оси движения вперед
    public void MoveLeft()
    {
        if (isStoped) { return; }

        if (lane != Lane.Left)
        {
            Vector3 pos = transform.position;


            if (_directionAxis == DirectionAxis.X)
            {

                SwitchLaneLeft();
                pos.x -= _laneWidth;
                transform.DOMoveX(pos.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            else if (_directionAxis == DirectionAxis.Z)
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
        if (isStoped) { return; }

        if (lane != Lane.Right)
        {
            Vector3 right = transform.position;

            if (_directionAxis == DirectionAxis.X)
            {

                SwitchLaneRight();
                right.x += _laneWidth;
                transform.DOMoveX(right.x, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
            else if (_directionAxis == DirectionAxis.Z)
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


        if (_directionAxis == DirectionAxis.Z)
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