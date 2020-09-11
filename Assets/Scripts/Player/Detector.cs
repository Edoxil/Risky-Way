using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    private PlayerReaction _reaction;

    public UnityEvent Detected;


    private void Start()
    {
        _reaction = GetComponentInParent<PlayerReaction>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

       // Луч напрален в низ и проверяет что под лезвием
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.transform.TryGetComponent(out Iinteractable interatable))
            {
                // Если под лезвием что то есть то отправляем события лезвию для удара
                Detected.Invoke();
                interatable.Interact();
                _reaction.ReactionFor(interatable);
            }
        }


        // Луч направлен вперед проверяет не столкнулся ли игрок с вертикальным препятствием
        ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out hit, 1f))
        {

            if (hit.transform.CompareTag("Vertical Obstacle"))
            {
                _reaction.VerticalCollision();
            }
        }
    }
}
