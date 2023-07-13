using UnityEngine;

class LoseTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Cart _))
        {
            GameStatus.OnLoseTrigger = true;
            collision.GetComponent<Cart>().DeathAnimation();
        }
    }
}

