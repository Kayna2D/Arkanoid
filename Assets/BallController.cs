using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(
            Random.Range(-0.8f, 0.8f),
            1
        ).normalized;

        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity =
            rb.linearVelocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();

            if (brick != null)
            {
                brick.TakeHit();

                FindFirstObjectByType<GameManager>().AdicionarPontos();
            }
        }
    }
}