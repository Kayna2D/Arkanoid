using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsRemaining = 1;
    private PowerUpType powerUpType = PowerUpType.None;
    private Sprite powerUpSprite;

    public void ConfigurePowerUp(PowerUpType type, Sprite itemSprite)
    {
        powerUpType = type;
        powerUpSprite = itemSprite;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
            renderer.color = type == PowerUpType.ExpandPaddle ? new Color(0.2f, 1f, 0.65f, 1f) : new Color(1f, 0.35f, 0.9f, 1f);
    }

    public void TakeHit()
    {
        hitsRemaining--;
        if (hitsRemaining <= 0)
        {
            if (powerUpType != PowerUpType.None)
                PowerUp.Create(powerUpType, transform.position, powerUpSprite, GetComponent<SpriteRenderer>()?.sprite);
            Destroy(gameObject);
        }
        else transform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }
}
