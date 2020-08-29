using UnityEngine;

public class CameraFalow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;


    private float _rotY;

    private void Start()
    {
        _rotY = transform.eulerAngles.y;

    }
    private void LateUpdate()
    {
        // Перемещаем камеру на определенном растоянии от цели
        //transform.position = _target.position - _offset;
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }


}

