using UnityEngine;

public class CargoTrigger : MonoBehaviour
{
    [SerializeField]
    private Cart _cart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStatus.OnLoseTrigger = true;
        _cart.DeathAnimation();
    }
}
