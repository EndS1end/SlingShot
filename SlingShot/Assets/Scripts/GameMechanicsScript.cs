using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;

public class GameMechanicsScript : MonoBehaviour
{

    [Header("Score Settings")]
    [SerializeField] private int _attempt = 0;
    [SerializeField] private int _maxLives = 5;
    [SerializeField] private int _lives;
    [SerializeField] private GameObject[] _liveSprites;
    [SerializeField] private int _CoinsOnScene;
    [SerializeField] private int _coinsLeft;
    

    [Header("Related Scripts")]
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private LaunchSystem _launchSys;
    [SerializeField] private InterfaceScripts _UICntrls;

    [Header("Related GameObjects")]
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _ballSprite;
    [SerializeField] private Transform _livesContainer;

    [Header("Sounds")]
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private AudioSource _hitSound;


    [Header("Related Ball Components")]
    private Rigidbody2D _ballrb;
    private TrailRenderer _ballTrail;

    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballrb = _ball.GetComponent<Rigidbody2D>();
        _ballTrail = _ball.GetComponent<TrailRenderer>();
        _launchSys = GameObject.FindGameObjectWithTag("Launcher").GetComponent<LaunchSystem>();
        _UICntrls = GameObject.FindGameObjectWithTag("UI").GetComponent<InterfaceScripts>();
    }

    public void StartGame()
    {
        
        LivesInit();
        LaunchSysActivate();
        _CoinsOnScene = _levelGenerator.LoadRandomLevel();
        _coinsLeft = _CoinsOnScene;
        _attempt = 0;
        _lives = _maxLives;
    }

    public void EndGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        _coinsLeft = 0;
        for(int i = 0; i < _livesContainer.childCount; i++) Destroy(_livesContainer.GetChild(i).gameObject);
        LaunchSysDeactivate();
    }

    public void RestartGame()
    {
        EndGame();
        StartGame();
    }

    public void LaunchSysActivate()
    {
        BallReset();
        _launchSys.enabled = true;
        
    }

    public void LaunchSysDeactivate()
    {
        _launchSys.enabled = false;
    }

    public bool IsOnLaunch()
    {
        return _launchSys.enabled;
    }

    public void BallReset()
    {
        _ballTrail.enabled = false;
        _ball.transform.position = _startPoint.transform.position;
        _ballrb.isKinematic = true;
        _ballrb.velocity = new Vector2(0, 0);
        _ballrb.angularVelocity = 0;
    }

    private void LivesInit()
    {
        _livesContainer = GameObject.Find("Lives").transform;
        float Offset = 1f; 
        _liveSprites = new GameObject[_maxLives];
        for(int i = 0; i < _maxLives; i++)
        {
            _liveSprites[i] = Instantiate(_ballSprite, _livesContainer);
            _liveSprites[i].transform.localPosition = new Vector3(0, i * Offset, 0);
        }
    }

    public void GotHit()
    {
        _hitSound.Play();
        _lives--;
        _attempt++;
        if (IsLost())
        {
            _UICntrls.Defeat();
            return;
        }
        _liveSprites[_lives].gameObject.SetActive(false);
        LaunchSysActivate();
    }

    public void CoinCollected()
    {
        _coinsLeft--;
        _coinSound.Play();
        if(_coinsLeft <= 0)
        {
            _ballrb.isKinematic = true;
            _ballrb.velocity = new Vector2(0, 0);
            _UICntrls.Victory();
        }
    }

    public int[] getStats()
    {
        int[] stats = new int[4];
        stats[0] = _maxLives;
        stats[1] = _lives;
        stats[2] = _attempt;
        stats[3] = _coinsLeft;
        return stats;
    }

    bool IsLost()
    {
        if (_lives <= 0) return true;
        return false;
    }
}
