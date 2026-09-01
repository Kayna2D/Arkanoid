using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);

            FindFirstObjectByType<GameManager>().PerderVida();
        }
    }
}