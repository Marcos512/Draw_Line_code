using System.Collections;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("Задержка перед переключением финиша")]
    private float _StopDelay;

    public bool AimFinished = false;

    [SerializeField]
    private Animator _closeAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Cart cart))
        {
            StartCoroutine(StopDelay(_StopDelay, cart));
            _closeAnim.SetTrigger("Close");
        }
    }

    private IEnumerator StopDelay(float delay, Cart cart)
    {
        cart.Finished();
        yield return new WaitForSeconds(delay);
        AimFinished = true;
        
    }
}
