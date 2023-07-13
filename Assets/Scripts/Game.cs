using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static event Action WinAction;
    public static event Action LoseAction;

    [SerializeField]
    private GameStatus _gameStatus;

    [SerializeField]
    private DrawLine _drawLine;

    [SerializeField]
    private List<Rigidbody2D> _rigidbodies;

    private Status _lastStatus;

    public bool LineIsDraw = false;

    private void Awake()
    {
        ToggleSimulate(false);
    }

    private void Update()
    {
        Status status;
        switch (status = _gameStatus.UpdateStatus())
        {
            case Status.Win: if (_lastStatus != Status.Win) WinGame(); break;
            case Status.Lose: if (_lastStatus != Status.Lose) LoseGame(); break;
            case Status.Play: if (_lastStatus != Status.Play) StartSimulation(); break;
            case Status.Prepare: Draw(); break;
        }
        _lastStatus = status;
    }

    private void Draw()
    {
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetMouseButtonDown(0))
        {
            _drawLine.TryStartDrawLine();
        }

        if (Input.GetMouseButton(0) && _drawLine.LineStartDraw)
        {
            _drawLine.TryAddNextPoint();
        }

        if (Input.GetMouseButtonUp(0) && _drawLine.LineStartDraw)
        {
            if (_drawLine.TryFinishDrawLine())
            {
                LineIsDraw = true;
            }
        }
    }

    private void LoseGame()
    {
        ToggleSimulate(false);
        LoseAction?.Invoke();

        StartCoroutine(AdsDelay(0.6f)); //реклама c задержкой
    }

    private void WinGame()
    {
        ToggleSimulate(false);
        WinAction?.Invoke();

        SaveProgress();
        StartCoroutine(AdsDelay(0.6f)); //реклама c задержкой
    }

    private void SaveProgress()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        int items = ItemCollecter.ItemsCount;
        YandexSavesManager.SaveLevelProgress(level, items);
    }

    private void StartSimulation()
    {
        ToggleSimulate(true);
    }

    private void ToggleSimulate(bool toggle)
    {
        foreach (var ob in _rigidbodies)
        {
            ob.bodyType = toggle ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        }
    }

    private IEnumerator AdsDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        YG.YandexGame.FullscreenShow(); //реклама
    }
}

