using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public Vector3 SpawnPosition;

    void OnTriggerEnter2D (Collider2D col)
    {
        PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.KillPlayer();
        }
    }
}
