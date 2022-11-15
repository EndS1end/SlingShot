using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class InterfaceScripts : MonoBehaviour
{
    [Header("Interface Panels")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _inGame;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _pauseMenu;

    [Header("Related Scripts")]
    [SerializeField] private GameMechanicsScript _GMScript;


    [Header("Sounds")]
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _loseSound;
    [SerializeField] private AudioSource _winSound;

    [Header("Sprites")]
    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private SVGImage _speedUpSpite;

    [Header("Other info")]
    [SerializeField] private bool ControlPhase;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _music.Play();
        _GMScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMechanicsScript>();
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        _speedUpSpite.color = Color.white;
        _loseScreen.SetActive(false);
        _victoryScreen.SetActive(false);
        _mainMenu.SetActive(false);
        _inGame.SetActive(true);
        _GMScript.StartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        _speedUpSpite.color = Color.white;
        _loseScreen.SetActive(false);
        _victoryScreen.SetActive(false);
        _inGame.SetActive(true);
        _GMScript.RestartGame();
    }

    public void ReturnToSling()
    {
        _GMScript.GotHit();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _speedUpSpite.color = Color.white;
        if (ControlPhase) _GMScript.LaunchSysActivate();
        _pauseMenu.SetActive(false);
        _inGame.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        PhaseToggle();
        _inGame.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    private void PhaseToggle()
    {
        if (_GMScript.IsOnLaunch())
        {
            _GMScript.LaunchSysDeactivate();
            ControlPhase = true;
        }
        else ControlPhase = false;
    }

    public void SpeedUpToggle()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 2.0f;
            _speedUpSpite.color = Color.green;
        }
        else
        {
            Time.timeScale = 1.0f;
            _speedUpSpite.color = Color.white;
        }
    }

    public void SoundSwitch(SVGImage SoundButton)
    {
        if (!AudioListener.pause)
        {
            AudioListener.pause = true;
            SoundButton.sprite = _musicOff;
        }
        else
        {
            AudioListener.pause = false;
            SoundButton.sprite = _musicOn;
        }
    }

    public void MenuExit()
    {
        Time.timeScale = 0.0f;
        _speedUpSpite.color = Color.white;
        _GMScript.EndGame();
        _inGame.SetActive(false);
        _pauseMenu.SetActive(false);
        _loseScreen.SetActive(false);
        _victoryScreen.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    //////////////////////////////////////////////////////////////
    
    public void Victory()
    {
        Time.timeScale = 0.0f;
        PhaseToggle();
        _winSound.Play();
        _inGame.SetActive(false);
        _victoryScreen.SetActive(true);
        ShowStats();
    }

    public void Defeat()
    {
        Time.timeScale = 0.0f;
        PhaseToggle();
        _loseSound.Play();
        _inGame.SetActive(false);
        _loseScreen.SetActive(true);
        ShowStats();
    }

    public void ShowStats()
    {
        TextMeshProUGUI Stats = GameObject.Find("Statistics").GetComponent<TextMeshProUGUI>();
        int[] Values = _GMScript.getStats();
        Stats.text = $"Статистика \n \n Жизней осталось : {Values[1]}/{Values[0]} \n Запусков совершено : {Values[2]} \n Монет осталось : {Values[3]}";
    }

    //////////////////////////////////////////////////////////////
    
}
