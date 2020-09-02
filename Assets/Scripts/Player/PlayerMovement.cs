using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    // Растояние на которое  премещаяется игрок влево/вправо 
    private float _laneWidth = 2f;

    private CharacterController _characterController;
    private Vector3 _forward = Vector3.zero;


    public bool isStoped = false;



    // Текущая полоса, на которой находится игрок
    public Lane lane = Lane.Mid;

    // Ось по которой происходит движение вперед
    private ForwardAxis _forwardAxis = ForwardAxis.X;

    public enum Lane
    {
        Left,
        Mid,
        Right
    }
    private enum ForwardAxis
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
        if (_forwardAxis == ForwardAxis.X)
        {
            _forward = Vector3.forward;
            _forward *= _speed * Time.deltaTime;
        }
        else if (_forwardAxis == ForwardAxis.Z)
        {
            _forward = Vector3.left;
            _forward *= _speed * Time.deltaTime;
        }

        // Перемещаем игрока вперед каждый кадр
        _characterController.Move(_forward);
    }




    // Поворот на 90 градусов и меняем ось движени вперед
    public void TurnLeftHandler()
    {
        

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= 90f;
        transform.DORotate(rot, 0.7f);
        transform.DORestart();
        DOTween.Play(transform);

        _forwardAxis = ForwardAxis.Z;


        
        AlignPosition();
    }




    // Движени влево/вправо в зависимости от оси движения вперед
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
            else if (_forwardAxis == ForwardAxis.Z)
            {
                SwitchLaneLeft();
                pos.z -= _laneWidth;
                transform.DOMoveZ(pos.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }
        }

        AlignPosition();
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
            else if (_forwardAxis == ForwardAxis.Z)
            {
                SwitchLaneRight();
                right.z += _laneWidth;
                transform.DOMoveZ(right.z, 0.2f);
                transform.DORestart();
                transform.DOPlay();
            }

        }
        AlignPosition();
    }


    #region EXPEREMENTAL
    private void AlignPosition()
    {
        Vector3 pos = transform.position;
        

        if (_forwardAxis == ForwardAxis.Z)
        {
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

            ///////////////////
            if (_forwardAxis == ForwardAxis.X)
            {
                if (lane == Lane.Mid)
                {

                    pos.x = 0f;
                    transform.DOMoveX(pos.x, 0.2f);
                    transform.DORestart();
                    transform.DOPlay();
                }
                if (lane == Lane.Left)
                {
                    pos.x = -2f;
                    transform.DOMoveX(pos.x, 0.2f);
                    transform.DORestart();
                    transform.DOPlay();
                }
                if (lane == Lane.Right)
                {
                    pos.x = 2f;
                    transform.DOMoveX(pos.x, 0.2f);
                    transform.DORestart();
                    transform.DOPlay();
                }


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
