using UnityEngine;
using UnityEngine.Events;

public class KeyBoardInput : MonoBehaviour
{

    public UnityEvent MoveLeft;
    public UnityEvent MoveRight;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight?.Invoke();
        }

    }
}
