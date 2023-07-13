using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private GameObject _sprite;

    [SerializeField]
    private AudioSource _audio;

    private bool _collected = false;

    public bool TryCollect()
    {
        if (!_collected)
        {
            _effect.Play();
            _audio.Play();
            _sprite.SetActive(false);
            _collected = true;
            Destroy(gameObject, 5);
            return true;
        }

        return false;
    }
}
