using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class RayShooter : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    private float _range = 0f;
    private bool isActive = false;




    void Update()
    {
        if (!isActive) return;

        if (isActive)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _range, Color.white);
            // Пускаем луч перед игроком и если попадаем в триггер поворота то поворачиваем игрока 
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, _range))
            {
                if (hit.transform.CompareTag("Trigger"))
                {
                    _playerMovement.TurnLeft();
                    TurnOff();
                }
            }
        }
    }

    public void TurnOn()
    {
        SetRange();
        isActive = true;
    }
    public void TurnOff()
    {
        isActive = false;
    }
    private void SetRange()
    {
        switch (_playerMovement.lane)
        {
            case PlayerMovement.Lane.Left:
                _range = 5f;
                break;
            case PlayerMovement.Lane.Mid:
                _range = 3f;
                break;
            case PlayerMovement.Lane.Right:
                _range = 2f;
                break;
            default:
                break;
        }
    }

}
