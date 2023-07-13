using System.Linq;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField, Tooltip("Направление и скорость движения")]
    private Vector2 _forceVector;

    [SerializeField]
    private Rigidbody2D _cartRigidbody2D;

    [SerializeField]
    private bool _onGraund = true;

    [SerializeField]
    private Animator _animator;

    private void FixedUpdate()
    {
        if (_onGraund)
            _cartRigidbody2D.AddRelativeForce(_forceVector);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _onGraund = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onGraund = false;
    }

    public void DeathAnimation() 
    {
        _cartRigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        _animator.enabled = true;
        _animator.SetTrigger("Death");
    }

    public void Finished()
    {
        _cartRigidbody2D.gameObject.SetActive(false);
    }
}
