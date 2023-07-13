using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField]
    private FinishTrigger[] _finishTriggers;

    private Status _lastStatus;
    private bool _gameStart = false;
    private bool _gameWin = false;

    public static bool OnLoseTrigger = false;

    private void Awake()
    {
        OnLoseTrigger = false;
        _lastStatus = Status.Prepare;
    }

    private void OnEnable()
    {
        YG.YandexGame.OpenVideoEvent += Pause;
        YG.YandexGame.CloseVideoEvent += SkipLevel;
    }

    private void OnDisable()
    {
        YG.YandexGame.OpenVideoEvent -= Pause;
        YG.YandexGame.CloseVideoEvent -= SkipLevel;
    }

    public Status UpdateStatus()
    {
        if (_lastStatus == Status.Win || _lastStatus == Status.Lose)
        {
            return _lastStatus;
        }

        if (!_gameStart)
        {
            return _lastStatus = Status.Prepare;
        }
        else if (ChekFinishTriggers() || _gameWin)
        {
            return _lastStatus = Status.Win;
        }
        else if (OnLoseTrigger)
        {
            return _lastStatus = Status.Lose;
        }
        else
        {
            return _lastStatus = Status.Play;
        }

    }

    public void GameStart() => _gameStart = true;

    private void Pause() => _gameStart = false;

    private void SkipLevel()
    {
        _gameStart = true;
        _gameWin = true;
    }

    private bool ChekFinishTriggers()
    {
        if (_finishTriggers.Length == 0)
        {
            Debug.Log("Добавте финиш");
            return false;
        }

        foreach (var finish in _finishTriggers)
        {
            if (!finish.AimFinished) return false;
        }

        return true;
    }
}
