using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;
        BallController ball = collision.GetComponent<BallController>();
        if (GameManager.instance != null) GameManager.instance.PerderBola(ball);
        else Destroy(collision.gameObject);
    }
}
