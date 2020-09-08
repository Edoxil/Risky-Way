using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    private Text _text = null;
    private float _timeElapsed = 0f;
    

    private void Start()
    {
        _text = GetComponent<Text>();
    }
    void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >=0.5f)
        {
            _text.enabled = true;
           
        }
        if(_timeElapsed >= 1f)
        {
            _text.enabled = false;
            _timeElapsed = 0f;
        }
    }
}
