using UnityEngine;

public class KeyController : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        PlayerController controller = col.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.PickupKey();
            Destroy(gameObject);
        }
    }
}
