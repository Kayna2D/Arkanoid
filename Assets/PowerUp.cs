using UnityEngine;

public enum PowerUpType { None, ExpandPaddle, MultiBall }

public class PowerUp : MonoBehaviour
{
    public PowerUpType Type { get; private set; }

    public static void Create(PowerUpType type, Vector3 position, Sprite icon, Sprite fallbackSprite)
    {
        GameObject item = new GameObject("PowerUp_" + type);
        item.transform.position = position;
        item.AddComponent<PowerUp>().Initialize(type, icon != null ? icon : fallbackSprite);
    }

    void Initialize(PowerUpType type, Sprite icon)
    {
        Type = type;
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = icon;
        renderer.color = type == PowerUpType.ExpandPaddle ? new Color(0.2f, 1f, 0.65f, 1f) : new Color(1f, 0.35f, 0.9f, 1f);
        renderer.sortingOrder = 2;
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(0.35f, 0.35f);
        Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        body.gravityScale = 0f;
        body.linearVelocity = Vector2.down * 2f;
    }

    void Update() { if (transform.position.y < -6f) Destroy(gameObject); }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance?.AplicarPowerUp(Type);
        Destroy(gameObject);
    }
}
