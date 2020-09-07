using DG.Tweening;
using UnityEngine;

public class PlayerReaction : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ReactionFor(Iinteractable obstacle)
    {
        if (obstacle.GetType().Equals(typeof(Bomb)))
        {
            TakeDamage();
        }
        if (obstacle.GetType().Equals(typeof(Heart)))
        {
            Debug.Log("Heart");
        }
        if (obstacle.GetType().Equals(typeof(MetalCrate)))
        {
            TakeDamage();
        }
    }

    public void VerticalCollision()
    {
        TakeDamage();
        ReturnToCheckPoint();
    }

    private void ReturnToCheckPoint()
    {
        _playerMovement.isStoped = true;
        _playerMovement.transform.DOMove(new Vector3(0f, 1.5f, 0f), 1f);
        _playerMovement.lane = PlayerMovement.Lane.Mid;
    }


    private void TakeDamage()
    {

        Blinking();
    }



    private void Blinking()
    {
        _renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");

        Color normal = Color.white;
        Color transparent = Color.white;

        transparent.a = 0.25f;

        Sequence seq = DOTween.Sequence();

        seq.onComplete += BlinkingFinished;
        for (int i = 0; i < 5; i++)
        {
            seq.Append(_renderer.material.DOColor(transparent, 0.1f));
            seq.AppendInterval(0.1f);
            seq.Join(_renderer.material.DOColor(normal, 0.1f));
            seq.AppendInterval(0.1f);
        }
        seq.Play();
    }
    private void BlinkingFinished()
    {
        _renderer.material.shader = Shader.Find("Mobile/Diffuse");
    }
}