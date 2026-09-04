using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance?.RegistrarBola(this);
        rb.linearVelocity = new Vector2(Random.Range(-0.8f, 0.8f), 1).normalized * speed;
    }

    void FixedUpdate()
    {
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.001f)
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Brick")) return;
        Brick brick = collision.gameObject.GetComponent<Brick>();
        if (brick == null) return;
        brick.TakeHit();
        FindFirstObjectByType<GameManager>().AdicionarPontos();
    }

    void OnDestroy() => GameManager.instance?.RemoverBola(this);
}
