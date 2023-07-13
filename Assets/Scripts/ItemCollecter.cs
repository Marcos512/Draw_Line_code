using UnityEngine;

class ItemCollecter : MonoBehaviour
{
    public static int ItemsCount;

    private void Awake()
    {
        ItemsCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            if (item.TryCollect())
                ItemsCount++;
        }
    }
}
