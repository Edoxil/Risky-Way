using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


public class PlayerReaction : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    private CameraManager _cameraManager = null;
    private Renderer _renderer = null;
    private Detector _detector = null;
    private Transform _player = null;
    private Vector3 _checkPoint = Vector3.zero;

    // Эвенты для обновления ресуров игрока
    public UnityEvent CoinColected;
    public UnityEvent HeartColected;
    public UnityEvent DamageTaken;


    private void Start()
    {
        _detector = GetComponentInChildren<Detector>();
        _cameraManager = CameraManager.GetInstance();
        _renderer = GetComponent<Renderer>();
    }

 
    // Обработка реакции на разные объекты на сцене и вызов нужного события в зависимости от типа объекта
    public void ReactionFor(Iinteractable obj)
    {
        if (obj.GetType().Equals(typeof(Heart)))
        {
            HeartColected?.Invoke();
            return;
        }
        if (obj.GetType().Equals(typeof(Coin)))
        {
            CoinColected?.Invoke();
            return;
        }
        if (obj.GetType().Equals(typeof(Crate)))
        {
            CoinColected?.Invoke();
            return;
        }
        // Если объект не относится к безопасным типам то наносится урон
        TakeDamage();

    }

    // Обработка столкновения с вертикальным объектом 
    public void VerticalCollision()
    {
        TakeDamage();
        ReturnToCheckPoint();
    }

    // возврат к последниму чекпоинту либо на старт НЕ ОКОНЧЕН  !!!!
    private void ReturnToCheckPoint()
    {
        _playerMovement.isStoped = true;
        _detector.enabled = false;
        
        _playerMovement.transform.DOMove(_checkPoint, 1f)
            .onComplete += () =>
            {
                _detector.enabled = true;
                _playerMovement.isStoped = false;
            };
        _playerMovement.lane = PlayerMovement.Lane.Mid;
    }
                




    // Отправляем событие о получении урона в PlayerResurces и анимация мигания ножа
    private void TakeDamage()
    {
        DamageTaken?.Invoke();
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

    // Реакция на смерть игрока. Останавливаем следование камеры и отправляем нож за пределы пути
    public void PlayerDiedHandler()
    {
        _playerMovement.isStoped = true;
        _player = _playerMovement.GetComponent<Transform>();
        _cameraManager.StopFolowing();

        Sequence seq = DOTween.Sequence();
        seq.Append(_player.DOMoveX(Vector3.right.x * 5f, 1f));
        seq.AppendInterval(0.3f);
        seq.Append(_player.DOMoveY(Vector3.down.y * 5f, 1f));
        seq.Play();
    }

    public void SetCheckPoint(Vector3 checkpoint)
    {
        checkpoint.y = 1.5f;
        _checkPoint = checkpoint;
    }
       
    public void GameStartHandler()
    {
        _checkPoint = new Vector3(0f, 1.5f, 0f);
    }
}