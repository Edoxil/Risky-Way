using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup = null;
    private Canvas _canvas = null;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        _canvasGroup.DOFade(1f, 0.5f).onComplete += FadeComplete;
    }
    public void Hide()
    {
        _canvasGroup.DOFade(0f, 0.5f).onComplete += FadeComplete;
    }

    private void FadeComplete()
    {
        if (_canvas.enabled)
        {
            _canvas.enabled = false;
        }
        else
        {
            _canvas.enabled = true;
        }
    }
}