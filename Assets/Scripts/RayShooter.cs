using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private KnifeMovement _knifeMovement;
    private float _range = 4f;

    private void Start()
    {
        _knifeMovement = GetComponent<KnifeMovement>();
    }

    void Update()
    {
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4f, Color.white);

        // Пускаем луч перед игроком и если попадаем в триггер поворота то поворачиваем игрока 
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, _range))
        {
            if(hit.transform.CompareTag("Trigger"))
            {
                _knifeMovement.TurnLeft();
            }
        }
    }
}
