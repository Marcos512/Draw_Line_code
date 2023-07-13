using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField]
    private GameObject _winForm, _loseForm, _gameButtons;

    [SerializeField]
    private Animator _winAnimator, _loseAnimator;

    private void OnEnable()
    {
        Game.WinAction += ShowWinForm;
        Game.LoseAction += ShowLoseForm;
    }

    private void OnDisable()
    {
        Game.WinAction -= ShowWinForm;
        Game.LoseAction -= ShowLoseForm;
    }

    public void ShowRewardedAd()
    {
        YG.YandexGame.RewVideoShow(0);
    }

    public void ShowWinForm()
    {
        int collectStars = ItemCollecter.ItemsCount;
        if (collectStars > 0)
        {
            var stars = _winForm.GetComponentsInChildren<Image>()
                .Where(image => image.name.StartsWith("Item"))
                .ToArray();
            for (int i = 0; i < collectStars; i++)
            {
                stars[i].color = Color.white;
            }
        }

        _winForm.SetActive(true);
        _loseForm.SetActive(false);
        _gameButtons.SetActive(false);

        _winAnimator.SetTrigger("Win");
    }

    public void ShowLoseForm()
    {
        _winForm.SetActive(false);
        _loseForm.SetActive(true);
        _gameButtons.SetActive(false);

        _loseAnimator.SetTrigger("Lose");
    }

    public void NextLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index + 1 == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(index + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
