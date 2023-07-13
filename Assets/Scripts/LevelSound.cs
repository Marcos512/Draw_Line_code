using UnityEngine;

public class LevelSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _winSound;

    [SerializeField]
    private AudioSource _winVoice;

    [SerializeField]
    private AudioSource _loseSound;

    [SerializeField]
    private AudioSource _loseVoice;

    private void OnEnable()
    {
        Game.WinAction += _winSound.Play;
        Game.WinAction += _winVoice.Play;
        Game.LoseAction += _loseSound.Play;
        Game.LoseAction += _loseVoice.Play;
    }

    private void OnDisable()
    {
        Game.WinAction -= _winSound.Play;
        Game.WinAction -= _winVoice.Play;
        Game.LoseAction -= _loseSound.Play;
        Game.LoseAction -= _loseVoice.Play;
    }
}
