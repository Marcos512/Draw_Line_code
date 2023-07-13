using UnityEngine;

public class Rocks : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rockRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Cart>())
        {
            _rockRb.simulated = true;
        }
    }
}
